using UnityEngine;

namespace Utility.UI
{
    public class RectTransformFitter : MonoBehaviour
    {
        private void Awake()
        {
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.Fit(FitType.Top);
        }
    }
}