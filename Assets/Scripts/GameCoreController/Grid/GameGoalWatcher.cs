
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

        private int _bombGoal;
        private int _bombCnt;
        public int BombGoal => _bombGoal;

        private List<AGridEffect> _effects;

        private bool _gameWon;

        public void Init()
        {
            _numberComb = new Dictionary<int, int>();
            _effects = new List<AGridEffect>();
            _gameWon = false;
        }

        public void AccountForEffect(AGridEffect effect)
        {
            if (_grassGoal > 0 && effect is GrassHealthChangeEffect grassEff)
            {
                if (grassEff.HealthLevel == 0)
                {
                    _grassCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.GRASS, Math.Max(_grassGoal - _grassCnt, 0), 0));
                }
            }
            else if (_honeyGoal > 0 && effect is HoneyHealthChangeEffect honeyEff)
            {
                if (honeyEff.HealthLevel == 0)
                {
                    _honeyCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.HONEY, Math.Max(_honeyGoal - _honeyCnt, 0), 0));
                }
                else if (honeyEff.JustSpawned) 
                {
                    // New honey spawned
                    _honeyGoal += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.HONEY, Math.Max(_honeyGoal - _honeyCnt, 0), 0));
                }
            }
            else if ((_boxGoal > 0 || _eggGoal > 0 || _bombGoal > 0) && effect is ChipDeletedEffect deletedEff) 
            { 
                if (_eggGoal > 0 && deletedEff.Chip is EggChip)
                {
                    _eggCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.EGG, Math.Max(_eggGoal - _eggCnt, 0), 0));
                }
                else if (_boxGoal > 0 && deletedEff.Chip is BoxChip)
                {
                    _boxCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.BOX, Math.Max(_boxGoal - _boxCnt, 0), 0));
                }
                else if (_bombGoal > 0 && deletedEff.Chip is BombChip)
                {
                    _bombCnt += 1;
                    _effects.Add(new GoalChangedEffect(GameGoals.BOMB, Math.Max(_bombGoal - _bombCnt, 0), 0));
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
                        _effects.Add(new GoalChangedEffect(GameGoals.NUMBER, Math.Max(_numberComb[numbId], 0), numbId));
                    }
                }
            }            

            if (IsGameWon() && !_gameWon)
            {
                _gameWon = true;
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
                _bombCnt >= _bombGoal &&
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

        public void SetCleanBombGoal(int cnt)
        {
            _bombGoal = cnt;
        }

    }
}