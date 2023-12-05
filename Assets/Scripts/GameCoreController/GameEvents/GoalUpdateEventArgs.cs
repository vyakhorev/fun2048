using GameInput;
using System.Collections.Generic;

namespace GameCoreController
{

    public class GoalUpdateEventArgs
    {
        public string GoalId;
        public int Cnt;

        public GoalUpdateEventArgs(string goalId, int cnt)
        {
            GoalId = goalId;
            Cnt = cnt;
        }
    }
}