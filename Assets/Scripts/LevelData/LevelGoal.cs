
using System;
using UnityEngine;

namespace LevelData
{
    public enum LevelGoalType
    {
        CLEAR_GRASS = 0,
        CLEAR_BOX = 1,
        CLEAR_EGG = 2,
        CLEAR_HONEY = 3,
        COMBINE_NUMBER = 4
    }

    [Serializable]
    public class LevelGoal
    {
        public LevelGoalType GoalType;
        public int Quantity;
        public int NumberToCombine;
    }
}