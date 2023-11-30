using System.Collections.Generic;
using UnityEngine;
using Utility.DataStructures;

namespace SO2048
{
    [CreateAssetMenu(menuName = "Fun2048/Grid/Board visual style")]
    public class SOBoardVisualStyle : ScriptableObject
    {
        [SerializeField] private GameObject _gridCellPrefab;
        public GameObject GridCellPrefab => _gridCellPrefab;

        [SerializeField] private GameObject _chipPrefab;
        public GameObject ChipPrefab => _chipPrefab;

        [SerializeField] private float _animSpeed = 0.2f;
        public float AnimSpeed => _animSpeed;

    }
}