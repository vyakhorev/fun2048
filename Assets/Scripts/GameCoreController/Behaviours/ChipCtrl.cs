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
    }
}
