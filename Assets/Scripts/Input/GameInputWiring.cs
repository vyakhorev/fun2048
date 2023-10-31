using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fun2048
{

    public class GameInputWiring : MonoBehaviour
    {
        public delegate void StartTouch(Vector2 postion, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 postion, float time);
        public event StartTouch OnEndTouch;

        private GameControls _gameControls;
        private InputContext _inputContext;
        private Camera _mainCamera;


        public void Init(InputContext inputContext)
        {
            _gameControls = new GameControls();
            _gameControls.Enable();
            _mainCamera = Camera.main;
            _inputContext = inputContext;
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
            InputEntity inputEntity = _inputContext.CreateEntity();
            inputEntity.AddSwipeAction(Fun2048.GridDirection.LEFT);
        }

        private void OnLeftCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void OnRight(InputAction.CallbackContext ctx)
        {
            InputEntity inputEntity = _inputContext.CreateEntity();
            inputEntity.AddSwipeAction(Fun2048.GridDirection.RIGHT);
        }

        private void OnRightCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void OnUp(InputAction.CallbackContext ctx)
        {
            InputEntity inputEntity = _inputContext.CreateEntity();
            inputEntity.AddSwipeAction(Fun2048.GridDirection.UP);
        }

        private void OnUpCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void OnDown(InputAction.CallbackContext ctx)
        {
            InputEntity inputEntity = _inputContext.CreateEntity();
            inputEntity.AddSwipeAction(Fun2048.GridDirection.DOWN);
        }

        private void OnDownCancelled(InputAction.CallbackContext ctx)
        {

        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (OnStartTouch != null)
            {
                OnStartTouch(
                    PrimaryPosition(),
                    (float)ctx.startTime
                );
            }

        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (OnEndTouch != null)
            {
                OnEndTouch(
                    PrimaryPosition(),
                    (float)ctx.time
                );
            }
        }

        public Vector2 PrimaryPosition()
        {
            return Utils.ScreenToWorld(
                _mainCamera,
                _gameControls.SwipeMap.PrimaryPosition.ReadValue<Vector2>()
            );
        }


    }
}