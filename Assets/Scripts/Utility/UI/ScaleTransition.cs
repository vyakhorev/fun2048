using DG.Tweening;
using UnityEngine;

namespace Utility.UI
{
    public struct ScaleTransition : ITransition<Transform>
    {
        [SerializeField]
        private float _targetValue;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private Ease _easeType;

        public Tween Play(Transform transform)
        {
            return transform.DOScale(_targetValue, _duration).SetEase(_easeType).SetUpdate(true);
        }
    }
}
