using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;
using Utility.UI;

namespace DebugCoreClient.UI
{
    public class VictoryPopUp : AutomaticPopUp
    {
        [SerializeField]
        private DebugStarter _starter;

        protected override void Awake()
        {
            base.Awake();
            _starter.OnWinEvt += Open;
            _closeButton.OnClicked += LoadMetaGameplay;
        }

        private void LoadMetaGameplay() => SceneManager.LoadScene(Constants.Scenes.Meta);
    }
}