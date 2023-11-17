using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fun2048
{
    public class Fun2048GameLoopFeatures : Feature
    {
        public Fun2048GameLoopFeatures(Contexts contexts) : base("Regular features")
        {
            Add(new InitPoolsSystem(contexts));
            Add(new ApplySwipeSystem(contexts));
            Add(new GridEffectsSystem(contexts));
            Add(new GridToWorldPositionSystem(contexts));
            Add(new SpawnChipViewSystem(contexts));
            Add(new ViewSynchSystem(contexts));
            Add(new ExpiredInputCleanupSystem(contexts));
            Add(new LifespanSystem(contexts));
        }
    }
}