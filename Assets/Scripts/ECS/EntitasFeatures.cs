using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fun2048
{
    public class EntitasFeatures : Feature
    {
        public EntitasFeatures(Contexts contexts) : base("Regular features")
        {
            Add(new ApplySwipeSystem(contexts));
            Add(new ViewSynchSystem(contexts));
            Add(new ExpiredCleanupSystem(contexts));
        }
    }
}