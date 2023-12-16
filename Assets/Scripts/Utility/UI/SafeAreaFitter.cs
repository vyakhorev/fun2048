using UnityEngine;

namespace Utility.UI
{
    public static class SafeAreaFitter 
    {
        public static void Fit(this RectTransform rectTransform)
        {
            Rect safeAreaRect = Screen.safeArea;
            Vector2 anchorMin = safeAreaRect.position;
            Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

        public static void Fit(this RectTransform rectTransform, FitType type, float safeAreaOffset = 0f, float rectTransformOffset = 0f)
        {
            Rect safeAreaRect = Screen.safeArea;

            if (type == FitType.Left || type == FitType.Right)
                safeAreaRect.width += safeAreaOffset;

            if(type == FitType.Top || type == FitType.Bottom)
                safeAreaRect.height += safeAreaOffset;

            Vector2 anchorMin = safeAreaRect.position;
            Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;

            switch (type)
            {
                case FitType.Left:
                    anchorMin.x = 1 - (safeAreaRect.width - rectTransformOffset) / Screen.width;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.x = rectTransform.anchorMax.x;
                    anchorMax.y = rectTransform.anchorMax.y;
                    break;
                case FitType.Right:
                    anchorMax.x = (safeAreaRect.width - rectTransformOffset) / Screen.width;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.y = rectTransform.anchorMax.y;
                    break;
                case FitType.Top:
                    anchorMax.y = (safeAreaRect.height - rectTransformOffset) / Screen.height;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMin.y = rectTransform.anchorMin.y - (1 - Screen.safeArea.height / Screen.height);
                    anchorMax.x = rectTransform.anchorMax.x;
                    break;
                case FitType.Bottom:
                    anchorMin.y = 1 - (safeAreaRect.height - rectTransformOffset) / Screen.height;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMax.y = rectTransform.anchorMax.y;
                    anchorMax.x = rectTransform.anchorMax.x;
                    break;
                case FitType.Horizontal:
                    anchorMin.x = 1 - (safeAreaRect.width - rectTransformOffset) / Screen.width;
                    anchorMax.x = (safeAreaRect.width - rectTransformOffset) / Screen.width;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.y = rectTransform.anchorMax.y;
                    break;
                case FitType.Vertical:
                    anchorMin.y = 1 - (safeAreaRect.height - rectTransformOffset) / Screen.height;
                    anchorMax.y = (safeAreaRect.height + rectTransformOffset) / Screen.height;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMax.x = rectTransform.anchorMax.x;
                    break;
                case FitType.VerticalStretch:
                    anchorMin.y = rectTransform.anchorMin.y - (Screen.height - safeAreaRect.height) / Screen.height;
                    anchorMax.y = rectTransform.anchorMax.y;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMax.x = rectTransform.anchorMax.x;
                    break;
            }

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }

    public enum FitType
    {
        Left,
        Right,
        Top,
        Bottom,
        Horizontal,
        Vertical,
        VerticalStretch
    }

    public enum OffsetType
    {
        RectTransformOffset,
        SafeAreaOffset
    }
}