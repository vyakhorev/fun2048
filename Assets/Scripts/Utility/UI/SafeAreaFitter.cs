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

        public static void Fit(this RectTransform rectTransform, FitType type, float offset = 0f)
        {
            Rect safeAreaRect = Screen.safeArea;

            if (safeAreaRect.height + 1 > Screen.height || safeAreaRect.width + 1 > Screen.width)
                return;

            safeAreaRect.height += (Screen.height - safeAreaRect.height) / 2;
            safeAreaRect.width += (Screen.width - safeAreaRect.width) / 2;

            if (type == FitType.Left || type == FitType.Right)
                safeAreaRect.width += offset;

            if(type == FitType.Top || type == FitType.Bottom)
                safeAreaRect.height += offset;

            Vector2 anchorMin = safeAreaRect.position;
            Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;

            switch (type)
            {
                case FitType.Left:
                    anchorMin.x = 1 - safeAreaRect.width / Screen.width;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.x = rectTransform.anchorMax.x;
                    anchorMax.y = rectTransform.anchorMax.y;
                    break;
                case FitType.Right:
                    anchorMax.x = safeAreaRect.width / Screen.width;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.y = rectTransform.anchorMax.y;
                    break;
                case FitType.Top:
                    anchorMax.y = safeAreaRect.height / Screen.height;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.x = rectTransform.anchorMax.x;
                    break;
                case FitType.Bottom:
                    anchorMin.y = 1 - safeAreaRect.height / Screen.height;
                    anchorMin.x = rectTransform.anchorMin.x;
                    anchorMax.y = rectTransform.anchorMax.y;
                    anchorMax.x = rectTransform.anchorMax.x;
                    break;
                case FitType.Horizontal:
                    anchorMin.x = 1 - safeAreaRect.width / Screen.width;
                    anchorMax.x = safeAreaRect.width / Screen.width;
                    anchorMin.y = rectTransform.anchorMin.y;
                    anchorMax.y = rectTransform.anchorMax.y;
                    break;
                case FitType.Vertical:
                    anchorMin.y = 1 - safeAreaRect.height / Screen.height;
                    anchorMax.y = safeAreaRect.height / Screen.height;
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
        Vertical
    }
}