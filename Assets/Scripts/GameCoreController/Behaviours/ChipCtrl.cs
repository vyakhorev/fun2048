using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Windows;

namespace GameCoreController
{
    /*
     * Attached to all the chips
     */
    public class ChipCtrl : MonoBehaviour
    {
        private CmpNumberChipVisuals _numberChipVisuals;
        private Dictionary<int, GameObject> _numbers;

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
            CacheNumberVisuals();

            _stoneChipVisuals = GetComponentInChildren<CmpStoneChipVisuals>(true);
            if (_stoneChipVisuals == null) throw new System.Exception("no CmpStoneChipVisuals");
            _eggChipVisuals = GetComponentInChildren<CmpEggChipVisuals>(true);
            if (_eggChipVisuals == null) throw new System.Exception("no CmpEggChipVisuals");
            _bubbleChipVisuals = GetComponentInChildren<CmpBubbleChipVisuals>(true);
            if (_bubbleChipVisuals == null) throw new System.Exception("no CmpBubbleChipVisuals");
            _boosterChipVisuals = GetComponentInChildren<CmpBoosterChipVisuals>(true);
            if (_boosterChipVisuals == null) throw new System.Exception("no CmpBoosterChipVisuals");
        }

        private void CacheNumberVisuals()
        {
            _numbers = new Dictionary<int, GameObject>();
            foreach (Transform numberRerpr in _numberChipVisuals.transform)
            {
                try
                {
                    int numb = Int32.Parse(numberRerpr.gameObject.name);
                    _numbers[numb] = numberRerpr.gameObject;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{numberRerpr.gameObject.name}'");
                }
            }
        }

        public void SetNumber(int number)
        {
            foreach (KeyValuePair<int, GameObject> entry in _numbers)
            {
                if (entry.Key == number)
                {
                    entry.Value.SetActive(true);
                } else
                {
                    entry.Value.SetActive(false);
                }
            }
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
