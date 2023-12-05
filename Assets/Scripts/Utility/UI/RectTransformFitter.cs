using UnityEngine;

namespace Utility.UI
{
    public class RectTransformFitter : MonoBehaviour
    {
        [SerializeField]
        private FitType _fitType = FitType.Top;

        private void Awake()
        {
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.Fit(_fitType);
        }
    }
}