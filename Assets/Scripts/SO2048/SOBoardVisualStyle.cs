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

        [SerializeField] private NumberSpriteMap _numberGoalSprites;
        public Sprite GetNumberSprite(int number) => _numberGoalSprites[number];

        [SerializeField] private Sprite _grassGoalSprite;
        public Sprite GrassGoalSprite => _grassGoalSprite;

        [SerializeField] private Sprite _honeyGoalSprite;
        public Sprite HoneyGoalSprite => _honeyGoalSprite;

        [SerializeField] private Sprite _boxGoalSprite;
        public Sprite BoxGoalSprite => _boxGoalSprite;

        [SerializeField] private Sprite _eggGoalSprite;
        public Sprite EggGoalSprite => _eggGoalSprite;

        [SerializeField] private float _animSpeed = 0.2f;
        public float AnimSpeed => _animSpeed;

    }

    [System.Serializable]
    public class NumberSpriteMap : SerializableHashMap<int, Sprite> { }

}