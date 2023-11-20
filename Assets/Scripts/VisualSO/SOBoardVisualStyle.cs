using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualSO
{
    [CreateAssetMenu(menuName = "Fun2048/Grid/Board visual style")]
    public class SOBoardVisualStyle : SerializedScriptableObject
    {
        public Dictionary<int, SONumberVisualStyle> NumberVisualStyles;
    }
}