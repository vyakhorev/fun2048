using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.UI;

namespace DebugCoreClient.UI
{
    public class DefeatPopUp : AutomaticPopUp
    {
        [SerializeField]
        private Button _replayButton;
        [SerializeField]
        private DebugStarter _starter;

        protected override void Awake()
        {
            base.Awake();
            _starter.OnLossEvt += Open;
            _replayButton.OnClicked += () => { _starter.RestartGame(); Close(); };
            _closeButton.OnClicked += LoadMetaGameplay;
        }

        private void LoadMetaGameplay() => SceneManager.LoadScene(Utility.Constants.Scenes.Meta);
    }
}