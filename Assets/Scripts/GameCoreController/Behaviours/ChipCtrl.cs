using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    public class ChipCtrl : MonoBehaviour
    {
        [SerializeField] private TextMesh _numberTextMesh;
        [SerializeField] private SpriteRenderer _tileSpriteRenderer;

        public void SetNumber(int number)
        {
            _numberTextMesh.text = number.ToString();
        }

        public void SetColor(Color color)
        {
            _tileSpriteRenderer.color = color;
        }
    }
}
