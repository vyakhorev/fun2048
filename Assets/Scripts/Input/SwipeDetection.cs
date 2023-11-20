
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{

    public class SwipeDetection : MonoBehaviour
    {

        [SerializeField] private float _minimumDistance = .2f;
        [SerializeField] private float _maximumTime = 1f;
        [SerializeField, Range(0f, 1f)] private float _directionThreshold = .9f;

        public delegate void OnSwipe(object sender, SwipeEventArgs e);
        public event OnSwipe OnSwipeEvent;

        private GameInputWiring _gameInputWiring;
        private Vector2 _startPosition;
        private float _startTime;
        private Vector2 _endPosition;
        private float _endTime;


        public void Init(GameInputWiring gameInputWiring)
        {
            _gameInputWiring = gameInputWiring;
            _gameInputWiring.OnStartTouch += SwipeStart;
            _gameInputWiring.OnEndTouch += SwipeEnd;
        }

        private void OnDisable()
        {
            _gameInputWiring.OnStartTouch -= SwipeStart;
            _gameInputWiring.OnEndTouch -= SwipeEnd;
        }

        private void SwipeStart(Vector2 position, float time)
        {
            _startPosition = position;
            _startTime = time;
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            _endPosition = position;
            _endTime = time;
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(_endPosition, _startPosition) >= _minimumDistance &&
                (_endTime - _startTime) <= _maximumTime)
            {
                Vector3 direction = _endPosition - _startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                InvokeSwipe(direction2D);
            }
        }

        private void InvokeSwipe(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
            {
                OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.UP));
            }
            else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
            {
                OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.DOWN));
            }
            else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
            {
                OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.LEFT));
            }
            else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
            {
                OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.RIGHT));
            }
        }

    }

}