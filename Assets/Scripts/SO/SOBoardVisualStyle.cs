using System.Collections.Generic;
using UnityEngine;
using Utility.DataStructures;

[CreateAssetMenu(menuName = "Fun2048/Grid/Board visual style")]
public class SOBoardVisualStyle : ScriptableObject
{
    [SerializeField]
    private NumberVisualsMap _numberVisualStyles;

    public SONumberVisualStyle GetNumberVisualStyle(int number) => _numberVisualStyles[number];
}

[System.Serializable]
public class NumberVisualsMap : SerializableHashMap<int, SONumberVisualStyle> { }