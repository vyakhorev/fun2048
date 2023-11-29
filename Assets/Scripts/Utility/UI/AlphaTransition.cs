using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.UI
{
    public struct AlphaTransition : ITransition<Image>
    {
        [SerializeField]
        private float _targetValue;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private Ease _easeType;

        public Tween Play(Image image)
        {
            return DOTween.ToAlpha(() => image.color, newColor => image.color = newColor, _targetValue, _duration).SetEase(_easeType).SetUpdate(true);
        }
    }
}
