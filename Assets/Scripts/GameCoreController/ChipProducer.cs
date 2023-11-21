using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualSO;

namespace GameCoreController
{
    /*
     * Helps with setting up new chips
     * TODO - move object pools here and make this thing responsible
     * for all construction / destruction. 
     */
    public class ChipProducer
    {

        private SOBoardVisualStyle _soBoardVisualStyle;

        public void Init(SOBoardVisualStyle soBoardVisualStyle)
        {
            _soBoardVisualStyle = soBoardVisualStyle;
        }

        public void UpdateNumberVisuals(ChipCtrl chipCtrl, int val)
        {
            SONumberVisualStyle style = _soBoardVisualStyle.NumberVisualStyles[val];
            chipCtrl.SetNumber(val);
            chipCtrl.SetColor(style.ChipColor);
        }


    }
}
