using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject parentMenu;

    [Header("HUD")]
    [SerializeField]
    private UIHUDController HUDControl;

    [Header("Panels")]
    [SerializeField]
    private UIPanel welcomePanel;
    [SerializeField]
    private UIGameStatesController gameStatesPanel;
    [SerializeField]
    private UIFinishGamePanel finishGamePanel;

    private void Start()
    {
        gameStatesPanel.OnPlayAction += GameManager.Instance.StartGame;
        gameStatesPanel.OnExitAction += GameManager.Instance.ExitApp;
        gameStatesPanel.Setup();

        finishGamePanel.Setup();
    }

    public void ShowWelcomePanel()
    {
        HUDControl.Hide();
        welcomePanel.Show();
        gameStatesPanel.Show();

        ShowParentPanels();
    }

    public void ShowFinishPanel()
    {
        finishGamePanel.Show();
        gameStatesPanel.Show();

        ShowParentPanels();
    }

    public void HidePanels()
    {
        HideParentPanels();

        welcomePanel.Hide();
        finishGamePanel.Hide();
        gameStatesPanel.Hide();
    }

    private void ShowParentPanels()
    {
        parentMenu.SetActive(true);
        HUDControl.Hide();
    }

    private void HideParentPanels()
    {
        parentMenu.SetActive(false);
        HUDControl.Show();
    }

    
}
