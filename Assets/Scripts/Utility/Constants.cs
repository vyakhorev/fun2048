using System.Collections;
using UnityEngine.SceneManagement;

namespace Utility
{
    public static class Constants
    {
        public static class Scenes
        {
            public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("0.Bootstrap");
            public static readonly int Loading = SceneUtility.GetBuildIndexByScenePath("1.Loading");
            public static readonly int Meta = SceneUtility.GetBuildIndexByScenePath("2.Meta");
            public static readonly int Core = SceneUtility.GetBuildIndexByScenePath("3.Core");
        }  
    }
}