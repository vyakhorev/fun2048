using UnityEngine;

namespace GameCoreController
{
    public class GoalChangedEffect : AGridEffect
    {
        public GameGoals Goal;
        public int NewCnt;
        public int Number;

        public GoalChangedEffect(GameGoals goal, int newCnt, int number)
        {
            Goal = goal;
            NewCnt = newCnt;
            Number = number;
        }

    }
}
