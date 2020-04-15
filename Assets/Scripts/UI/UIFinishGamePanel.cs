using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishGamePanel : UIPanel
{
    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text copyScoreFrom;

    public override void Show()
    {
        scoreTxt.text = copyScoreFrom.text;

        base.Show();
    }
}
