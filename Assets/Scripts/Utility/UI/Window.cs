using UnityEngine;

namespace Utility.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField]
        protected Button _openButton;
        [SerializeField]
        protected Button _closeButton;
        [SerializeField]
        protected Canvas _canvas;

        protected virtual void Awake()
        {
            Close();
            _openButton.OnClicked += Open;
            _closeButton.OnClicked += Close;
        }

        private void Open() => _canvas.enabled = true;

        protected void Close() => _canvas.enabled = false;
    }
}