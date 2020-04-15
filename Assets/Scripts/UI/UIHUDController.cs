using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDController : UIPanel
{
    [SerializeField]
    private Text scoreTxt;

    private void Start()
    {
        ScoreController.Instance.OnScoreUpdated += SetTxtScore;
    }

    public void SetTxtScore(float value)
    {
        scoreTxt.text = value.ToString();
    }
}
