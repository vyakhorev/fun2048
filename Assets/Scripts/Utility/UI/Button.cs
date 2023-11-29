using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility.UI
{
    public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public event Action OnClicked;

        [SerializeField]
        private ScaleTransition _inTransition;
        [SerializeField]
        private ScaleTransition _outTransition;

        private Tween _currentTween;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _currentTween?.Complete();
            _currentTween = _inTransition.Play(transform);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _currentTween?.Complete();
            _currentTween = _outTransition.Play(transform);
        }
    }
}