using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility.UI;

namespace Meta.Level.UI
{
    public class LevelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public event Action<int> OnClicked;

        private static readonly string s_buttonNameFormat = "Уровень {0}";

        [SerializeField]
        private TextMeshProUGUI _buttonName;
        [SerializeField]
        private ScaleTransition _inTransition;
        [SerializeField]
        private ScaleTransition _outTransition;

        private Tween _currentTween;
        private int _currentIndex;

        public void SetIndex(int index)
        {
            _currentIndex = index;
            _buttonName.text = string.Format(s_buttonNameFormat, index.ToString());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(_currentIndex);
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