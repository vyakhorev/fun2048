using UnityEngine;
using UnityEngine.UI;

namespace Utility.UI
{
    public class FlexibleGridLayout : LayoutGroup
    {
        [SerializeField]
        private int _columns;
        [SerializeField]
        private int _rows;
        [SerializeField]
        private Vector2 _cellSize;
        [SerializeField]
        private Vector2 _spacing;

        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputHorizontal();

            float sqrRt = Mathf.Sqrt(transform.childCount);
            int ceiled = Mathf.CeilToInt(sqrRt);
            _columns = ceiled;
            _rows = ceiled;

            if (_columns == 0 || _rows == 0)
                return;

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float cellWidth  = parentWidth / _columns - _spacing.x / _columns * 2f - padding.left / _columns - padding.right / _columns;
            float cellHeight = parentHeight / _columns - _spacing.y /_rows * 2f - padding.top / _rows - padding.bottom / _rows;

            _cellSize = new Vector2(cellWidth, cellHeight);

            int columnCount;
            int rowCount;

            int childrenCount = rectChildren.Count;

            for (int i = 0; i < childrenCount; i++)
            {
                rowCount = i / _columns;
                columnCount = i % _columns;

                RectTransform child = rectChildren[i];

                float x = _cellSize.x * columnCount + _spacing.x * columnCount + padding.right;
                float y = _cellSize.y * rowCount + _spacing.y * rowCount + padding.top;

                SetChildAlongAxis(child, 0, x, _cellSize.x);
                SetChildAlongAxis(child, 1, y, _cellSize.y);
            }
        }

        public override void SetLayoutHorizontal() { }

        public override void SetLayoutVertical() { }
    }
}