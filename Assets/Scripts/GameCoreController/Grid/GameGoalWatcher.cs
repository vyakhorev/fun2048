
using LevelData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    public class GameGoalWatcher
    {
        private Dictionary<int, int> _numberComb;
        public Dictionary<int, int> NumberComb => _numberComb;

        private int _grassGoal;
        private int _grassCnt;
        public int GrassGoal => _grassGoal;

        private int _honeyGoal;
        private int _honeyCnt;
        public int HoneyGoal => _honeyGoal;

        private int _eggGoal;
        private int _eggCnt;
        public int EggGoal => _eggGoal;

        private int _boxGoal;
        private int _boxCnt;
        public int BoxGoal => _boxGoal;

        private List<AGridEffect> _effects;

        public void Init()
        {
            _numberComb = new Dictionary<int, int>();
            _effects = new List<AGridEffect>();
        }

        public void AccountForEffect(AGridEffect effect)
        {
            if (_grassGoal > 0 && effect is GrassHealthChangeEffect)
            {
                _grassCnt += 1;
                _effects.Add(new GoalChangedEffect(GameGoals.GRASS, _grassGoal - _grassCnt, 0));
            } 
            else if (_honeyGoal > 0 && effect is HoneyHealthChangeEffect)
            {
                _honeyCnt += 1;
                _effects.Add(new GoalChangedEffect(GameGoals.HONEY, _honeyGoal - _honeyCnt, 0));
            }
            else if ((_boxGoal > 0 || _eggGoal > 0) && effect is ChipDeletedEffect deletedEff) 
            { 
                if (_eggGoal > 0 && deletedEff.Chip is EggChip)
                {
                    _eggCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.EGG, _eggGoal - _eggCnt, 0));
                }
                else if (_boxGoal > 0 && deletedEff.Chip is BoxChip)
                {
                    _boxCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.BOX, _boxGoal - _boxCnt, 0));
                }
            }
            else if (_numberComb.Count > 0 && effect is ChipsMergeEffect mergeEff)
            {
                if (mergeEff.ChipFrom is NumberChip numbChip) 
                {
                    var numbId = numbChip.GetNumericValue();
                    if (_numberComb.ContainsKey(numbId))
                    {
                        _numberComb[numbId] -= 1;
                        _effects.Add(new GoalChangedEffect(GameGoals.NUMBER, _numberComb[numbId], numbId));
                    }
                }
            }            

            if (IsGameWon())
            {
                _effects.Add(new GameWonEffect());
            }

        }

        public List<AGridEffect> GetEffects()
        {
            return _effects;
        }

        public void ResetEffects()
        {
            _effects = new List<AGridEffect>();
        }

        private bool IsGameWon()
        {

            bool chipsCombined = true;
            foreach(var chipCnt in _numberComb)
            {
                if (chipCnt.Value > 0)
                {
                    chipsCombined = false;
                    break;
                }
            }

            return _grassCnt >= _grassGoal &&
                _honeyCnt >= _honeyGoal &&
                _eggCnt >= _eggGoal &&
                _boxCnt >= _boxGoal &&
                chipsCombined;
        }

        public void SetCleanGrassGoal(int cnt)
        {
            _grassGoal = cnt;
        }

        public void SetCleanHoneyGoal(int cnt) 
        { 
            _honeyGoal = cnt;
        }

        public void SetCleanEggGoal(int cnt)
        {
            _eggGoal = cnt;
        }

        public void SetCleanBoxGoal(int cnt)
        {
            _boxGoal = cnt;
        }

        public void SetCombineNumbersGoal(int number, int cnt)
        {
            _numberComb[number] = cnt;
        }

    }
}