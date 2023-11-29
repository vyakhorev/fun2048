using System.Collections.Generic;
using UnityEngine;
using Utility.DataStructures;

[CreateAssetMenu(menuName = "Fun2048/Grid/Board visual style")]
public class SOBoardVisualStyle : ScriptableObject
{
    public NumberVisualsMap NumberVisualStyles;
}

[System.Serializable]
public class NumberVisualsMap : SerializableHashMap<int, SONumberVisualStyle> { }