using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberChipAuth : MonoBehaviour
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