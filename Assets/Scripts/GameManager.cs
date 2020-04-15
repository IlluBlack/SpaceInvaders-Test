using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState gameState;

    [SerializeField]
    private UIController UIControl;
    [SerializeField]
    private PlayerController playerControl;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        Welcome();
    }

    private void Welcome()
    {
        PauseTimeScale();

        UIControl.ShowWelcomePanel();
        gameState = GameState.Waiting;
    }

    public void StartGame()
    {
        RestartControllers();

        UIControl.HidePanels();
        gameState = GameState.Playing;

        PlayTimeScale();
    }

    private void RestartControllers()
    {
        playerControl.Restart();
        ScoreController.Instance.Restart();
        EnemiesController.Instance.Restart();
        ObjectPooler.Instance.Restart();
    }


    public void FinishGame()
    {
        PauseTimeScale();

        UIControl.ShowFinishPanel();
        gameState = GameState.Waiting;
    }

    public void ExitApp()
    {
        Application.Quit();
        Debug.Log("Exit App");
    }


    public bool IsPlaying()
    {
        return (gameState == GameState.Playing) ? true : false;
    }

    private void PauseTimeScale()
    {
        Time.timeScale = 0f;
    }

    private void PlayTimeScale()
    {
        Time.timeScale = 1f;
    }

    
}

public enum GameState
{
    Waiting, //player has not started game
    Playing,
    Pause,
}
