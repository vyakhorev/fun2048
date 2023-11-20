using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fun2048/Grid/Board visual style")]
public class SOBoardVisualStyle : ScriptableObject
{
    public Dictionary<int, SONumberVisualStyle> NumberVisualStyles;
}