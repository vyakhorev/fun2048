using UnityEngine;

namespace Utility.UI
{
    public abstract class AutomaticPopUp : MonoBehaviour
    {
        [SerializeField]
        protected Button _closeButton;

        [SerializeField]
        private Canvas _canvas;

        protected virtual void Awake()
        {
            _closeButton.OnClicked += Close;
            Close();
        }

        protected virtual void Open()
        {
            _canvas.enabled = true;
        }

        protected virtual void Close()
        {
            _canvas.enabled = false;
        }
    }
}