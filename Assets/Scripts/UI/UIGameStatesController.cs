using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIGameStatesController : UIPanel
{
    public event Action OnPlayAction;
    public event Action OnExitAction;

    [SerializeField]
    private Button playBtn;
    [SerializeField]
    private Button exitBtn;

    public override void Setup()
    {
        playBtn.onClick.AddListener(Play);
        exitBtn.onClick.AddListener(Exit);
    }

    private void Play()
    {
        if (OnPlayAction != null)
            OnPlayAction();
    }

    private void Exit()
    {
        if (OnExitAction != null)
            OnExitAction();
    }
}
