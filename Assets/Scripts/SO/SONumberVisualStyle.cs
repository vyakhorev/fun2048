using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Fun2048/Grid/Number visual style")]
public class SONumberVisualStyle : ScriptableObject
{
    public Color ChipColor => _chipColor;
    public Color FontColor => _fontColor;

    [SerializeField] private Color _chipColor;
    [SerializeField] private Color _fontColor;
}