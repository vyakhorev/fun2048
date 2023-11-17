using Entitas;
using System;
using UnityEngine;

namespace Fun2048
{
    /*
     * Shall download addressables later
     */
    public class GameAssetsManager : MonoBehaviour
    {

        [SerializeField] private GameObject _numberChipPrefab;
        [SerializeField] private GameObject _gridCellPrefab;
        [SerializeField] private SOBoardVisualStyle _SOBoardVisualStyle;

        public static GameAssetsManager Instance;

        public void OnGameStart()
        {
            Instance = this;
        }

        public GameObject GetNumberChipPrefab()
        {
            return _numberChipPrefab;
        }

        public GameObject GetGridCellPrefab()
        {
            return _gridCellPrefab;
        }

        public SOBoardVisualStyle GetSOBoardVisualStyle()
        {
            return _SOBoardVisualStyle;
        }

    }
}