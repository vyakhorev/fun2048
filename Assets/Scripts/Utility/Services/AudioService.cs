using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility.Containers;
using Object = UnityEngine.Object;

namespace Utility.Services
{
    public class AudioService : ILoadUnit, IInitializable
    {
        private AudioSource _soundFXPrefab;
        private AudioSource _musicFXPrefab;
        private AudioSource _musicFXSource;
        private Pool<AudioSource> _soundFXPool;

        public void Initialize()
        {
            Func<AudioSource> initializer = new(() => Object.Instantiate(_soundFXPrefab));
            Func<AudioSource, bool> isFreePredicate = new((audioSource) => !audioSource.isPlaying);
            Action<AudioSource> returnToPoolAction = new((audioSource) => audioSource.Stop());

            _soundFXPool = new Pool<AudioSource>(initializer, isFreePredicate, returnToPoolAction, 3);
            _musicFXSource = Object.Instantiate(_musicFXPrefab);
        }

        UniTask ILoadUnit.Load()
        {
            _soundFXPrefab = AssetService.Resources.Load<AudioSource>("Prefabs/soundFX");
            _musicFXPrefab = AssetService.Resources.Load<AudioSource>("Prefabs/musicFX");
            return UniTask.CompletedTask;
        }

        public void PlayAudioClip(AudioClip clip, AudioType type, bool? isLooped = null, float? crossFadeValue = null)
        {
            switch (type)
            {
                case AudioType.SoundFX:
                    AudioSource freeAudioSource = _soundFXPool.GetFreePooledObject();
                    freeAudioSource.clip = clip;
                    freeAudioSource.Play();
                    break;
                case AudioType.MusicFX:
                    PlayMusicClip(clip, isLooped.GetValueOrDefault(), crossFadeValue.GetValueOrDefault());
                    break;
            }
        }

        public async void PlayAudioClipDelayed(AudioClip clip, AudioType type, float delay, bool? isLooped = null, float? crossFadeValue = null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            PlayAudioClip(clip, type, isLooped, crossFadeValue);
        }

        private void PlayMusicClip(AudioClip clip, bool isLooped, float crossFadeValue)
        {
            if (_musicFXSource.isPlaying)
                CrossFade(clip, isLooped, crossFadeValue).Forget();
            else
                FadeIn(clip, isLooped, crossFadeValue).Forget();
        }

        private async UniTaskVoid CrossFade(AudioClip clip, bool isLooped, float crossFadeValue)
        {
            float fadeHalf = crossFadeValue * 0.5f;

            await FadeOut(fadeHalf);
            await FadeIn(clip, isLooped, fadeHalf);
        }

        private async UniTask FadeOut(float fadeDuration)
        {
            float lerp = fadeDuration;

            while (lerp >= 0f)
            {
                lerp -= Time.deltaTime;
                _musicFXSource.volume = lerp / fadeDuration;
                await UniTask.Yield();
            }

            _musicFXSource.Stop();
        }

        private async UniTask FadeIn(AudioClip clip, bool isLooped, float fadeInDuration)
        {
            _musicFXSource.volume = 0f;
            _musicFXSource.clip = clip;
            _musicFXSource.loop = isLooped;
            _musicFXSource.Play();

            float lerp = 0f;

            while (lerp <= fadeInDuration)
            {
                lerp += Time.deltaTime;
                _musicFXSource.volume = lerp / fadeInDuration;
                await UniTask.Yield();
            }
        }
    }

    public enum AudioType
    {
        SoundFX = 0,
        MusicFX = 1
    }
}