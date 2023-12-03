using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelData
{
    [Serializable]
    public class RootLevelData
    {
        public int MaxTurns;
        public List<LevelGoal> Goals;
        public BoardData Board;
    }
}