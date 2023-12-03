using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DebugCoreClient
{
    public class GoalVisCntr : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public void SetupGoal(Sprite goalSprite, int goalCnt)
        {
            _image.sprite = goalSprite;
            UpdateGoal(goalCnt);
        }

        public void UpdateGoal(int goalCnt)
        {
            _text.text = goalCnt.ToString();
        }

    }
}
