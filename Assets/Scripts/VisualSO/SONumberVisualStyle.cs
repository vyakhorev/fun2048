using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualSO
{
    [CreateAssetMenu(menuName = "Fun2048/Grid/Number visual style")]
    public class SONumberVisualStyle : SerializedScriptableObject
    {
        [SerializeField] public Color ChipColor;
        [SerializeField] public Color FontColor;
    }
}
