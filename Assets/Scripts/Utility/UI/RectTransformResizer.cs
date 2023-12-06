using UnityEngine;

namespace Utility.UI
{
    public class RectTransformResizer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransformAbove;

        private void Start()
        {
            RectTransform rectTransform = transform as RectTransform;
            Vector2 newAnchorMax = new(rectTransform.anchorMax.x, _rectTransformAbove.anchorMin.y);
            rectTransform.anchorMax = newAnchorMax;
        }
    }
}