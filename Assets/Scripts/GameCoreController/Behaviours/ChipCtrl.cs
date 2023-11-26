using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    /*
     * Attached to all the chips
     */
    public class ChipCtrl : MonoBehaviour
    {
        private CmpNumberChipVisuals _numberChipVisuals;
        private TextMesh _numberTextMesh;
        private SpriteRenderer _numberSpriteRenderer;

        private CmpStoneChipVisuals _stoneChipVisuals;
        private CmpEggChipVisuals _eggChipVisuals;
        private CmpBubbleChipVisuals _bubbleChipVisuals;
        private CmpBoosterChipVisuals _boosterChipVisuals;

        public void Awake()
        {
            InitHierarchy();
        }

        public void InitHierarchy()
        {
            _numberChipVisuals = GetComponentInChildren<CmpNumberChipVisuals>(true);
            if (_numberChipVisuals == null) throw new System.Exception("no CmpNumberChipVisuals");
            _numberTextMesh = _numberChipVisuals.GetComponentInChildren<TextMesh>();
            _numberSpriteRenderer = _numberChipVisuals.GetComponentInChildren<SpriteRenderer>();

            _stoneChipVisuals = GetComponentInChildren<CmpStoneChipVisuals>(true);
            if (_stoneChipVisuals == null) throw new System.Exception("no CmpStoneChipVisuals");
            _eggChipVisuals = GetComponentInChildren<CmpEggChipVisuals>(true);
            if (_eggChipVisuals == null) throw new System.Exception("no CmpEggChipVisuals");
            _bubbleChipVisuals = GetComponentInChildren<CmpBubbleChipVisuals>(true);
            if (_bubbleChipVisuals == null) throw new System.Exception("no CmpBubbleChipVisuals");
            _boosterChipVisuals = GetComponentInChildren<CmpBoosterChipVisuals>(true);
            if (_boosterChipVisuals == null) throw new System.Exception("no CmpBoosterChipVisuals");
        }

        public void SetNumber(int number)
        {
            _numberTextMesh.text = number.ToString();
        }

        public void SetColor(Color color)
        {
            _numberSpriteRenderer.color = color;
        }

        public void SpawnAsNumber()
        {
            _numberChipVisuals.gameObject.SetActive(true);
            _stoneChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _boosterChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsStone()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _stoneChipVisuals.gameObject.SetActive(true);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _boosterChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsEgg()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _stoneChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(true);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _boosterChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsBubble()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _stoneChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(true);
            _boosterChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsBooster()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _stoneChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _boosterChipVisuals.gameObject.SetActive(true);
        }


    }
}
