using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Utility.Services;

namespace Bootstrap
{
    public class ApplicationConfiguration : ILoadUnit
    {
        public UniTask Load()
        {
            Application.targetFrameRate = (int)Math.Round(Screen.currentResolution.refreshRateRatio.value);
            return UniTask.CompletedTask;
        }
    }
}