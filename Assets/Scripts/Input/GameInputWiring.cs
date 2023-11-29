using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{

    public class GameInputWiring : MonoBehaviour
    {
        public delegate void StartTouch(Vector2 postion, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 postion, float time);
        public event StartTouch OnEndTouch;

        public delegate void OnSwipe(object sender, SwipeEventArgs e);
        public event OnSwipe OnSwipeEvent;

        private GameControls _gameControls;
        private Camera _mainCamera;


        public void Init()
        {
            _gameControls = new GameControls();
            _gameControls.Enable();
            _mainCamera = Camera.main;
        }

        private void OnDisable()
        {
            _gameControls.Disable();
        }

        private void Start()
        {

            _gameControls.SwipeMap.Left.performed += ctx => OnLeft(ctx);
            _gameControls.SwipeMap.Left.canceled += ctx => OnLeftCancelled(ctx);

            _gameControls.SwipeMap.Right.performed += ctx => OnRight(ctx);
            _gameControls.SwipeMap.Right.canceled += ctx => OnRightCancelled(ctx);

            _gameControls.SwipeMap.Up.performed += ctx => OnUp(ctx);
            _gameControls.SwipeMap.Up.canceled += ctx => OnUpCancelled(ctx);

            _gameControls.SwipeMap.Down.performed += ctx => OnDown(ctx);
            _gameControls.SwipeMap.Down.canceled += ctx => OnDownCancelled(ctx);

            _gameControls.SwipeMap.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
            _gameControls.SwipeMap.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

        }

        private void OnLeft(InputAction.CallbackContext ctx)
        {
            OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.LEFT));
        }

        private void OnLeftCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void OnRight(InputAction.CallbackContext ctx)
        {
            OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.RIGHT));
        }

        private void OnRightCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void OnUp(InputAction.CallbackContext ctx)
        {
            OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.UP));
        }

        private void OnUpCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void OnDown(InputAction.CallbackContext ctx)
        {
            OnSwipeEvent?.Invoke(this, new SwipeEventArgs(SwipeDirection.DOWN));
        }

        private void OnDownCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnStartTouch?.Invoke(
                PrimaryPosition(),
                (float)ctx.startTime
            );

        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnEndTouch?.Invoke(
                PrimaryPosition(),
                (float)ctx.time
            );
        }

        public Vector2 PrimaryPosition()
        {
            return Utils.UtilFunc.ScreenToWorld(
                _mainCamera,
                _gameControls.SwipeMap.PrimaryPosition.ReadValue<Vector2>()
            );
        }


    }
}