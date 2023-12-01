using System;
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
        private Dictionary<int, GameObject> _numbers;

        private CmpBoxChipVisuals _boxChipVisuals;
        private Dictionary<int, GameObject> _boxByHealh;
        
        private CmpEggChipVisuals _eggChipVisuals;
        private Dictionary<int, GameObject> _eggByHealh;

        private CmpBubbleChipVisuals _bubbleChipVisuals;
        private CmpBombChipVisuals _bombChipVisuals;
        
        public void Awake()
        {
            InitHierarchy();
        }

        public void InitHierarchy()
        {
            _numberChipVisuals = GetComponentInChildren<CmpNumberChipVisuals>(true);
            if (_numberChipVisuals == null) throw new System.Exception("no CmpNumberChipVisuals");
            CacheNumberVisuals();

            _boxChipVisuals = GetComponentInChildren<CmpBoxChipVisuals>(true);
            if (_boxChipVisuals == null) throw new System.Exception("no CmpStoneChipVisuals");
            CacheBoxChipVisuals();

            _eggChipVisuals = GetComponentInChildren<CmpEggChipVisuals>(true);
            if (_eggChipVisuals == null) throw new System.Exception("no CmpEggChipVisuals");
            CacheEggChipVisuals();

            _bubbleChipVisuals = GetComponentInChildren<CmpBubbleChipVisuals>(true);
            if (_bubbleChipVisuals == null) throw new System.Exception("no CmpBubbleChipVisuals");
            _bombChipVisuals = GetComponentInChildren<CmpBombChipVisuals>(true);
            if (_bombChipVisuals == null) throw new System.Exception("no CmpBoosterChipVisuals");
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

        private void CacheBoxChipVisuals()
        {
            _boxByHealh = new Dictionary<int, GameObject>();
            foreach (Transform healthRepr in _boxChipVisuals.transform)
            {
                try
                {
                    int numb = Int32.Parse(healthRepr.gameObject.name);
                    _boxByHealh[numb] = healthRepr.gameObject;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{healthRepr.gameObject.name}'");
                }
            }
        }

        private void CacheEggChipVisuals()
        {
            _eggByHealh = new Dictionary<int, GameObject>();
            foreach (Transform healthRepr in _eggChipVisuals.transform)
            {
                try
                {
                    int numb = Int32.Parse(healthRepr.gameObject.name);
                    _eggByHealh[numb] = healthRepr.gameObject;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{healthRepr.gameObject.name}'");
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
            _boxChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _bombChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsBox()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _boxChipVisuals.gameObject.SetActive(true);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _bombChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsEgg()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _boxChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(true);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _bombChipVisuals.gameObject.SetActive(false);
        }

        // Not in use
        public void SpawnAsBubble()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _boxChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(true);
            _bombChipVisuals.gameObject.SetActive(false);
        }

        public void SpawnAsBomb()
        {
            _numberChipVisuals.gameObject.SetActive(false);
            _boxChipVisuals.gameObject.SetActive(false);
            _eggChipVisuals.gameObject.SetActive(false);
            _bubbleChipVisuals.gameObject.SetActive(false);
            _bombChipVisuals.gameObject.SetActive(true);
        }


    }
}
