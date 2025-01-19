using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IOnGameOver, IOnGamePause, IOnGameStart<IOnGameStates>, IOnStageOver, IOnStageStart
{
    GameState gameState;
    IOnGameStates gameRunner;
    [SerializeField]
    Initializer initializer;

    [Header("--------------------Cong--------------------")]    
    public DataGamePlay dataGamePlay;
    public GaneData gameData;

    public Action onGameOverAction => () => SetGameState(GameState.None);

    public Action onGamePauseAction => () =>
    {
        Time.timeScale = 0;
        SetGameState(GameState.None);
    };

    public Action onStageOverAction => () => SetGameState(GameState.Running);

    public Action onStageStartAction => () => SetGameState(GameState.Running);

    public Action<IOnGameStates> onGameStartAction => param => gameRunner = param;

    private void Start()
    {
        dataGamePlay.StartDataGamePlay();
        LoadDataGame();
        DataPlayer.Instance.StartPlayerData();
        SetPanelOptions();
        initializer.InjectAllAtGameStart();
        SetGameState(GameState.StageStart);
    }

    private void Update()
    {
        //DataPlayer.Instance.LevelUp(1);
        switch (gameState)
        {
            case GameState.None:
                return;
            case GameState.Running:
                gameRunner.OnGameRunning();
                return;
            case GameState.Pause:
                gameRunner.OnGamePause();
                return;
            case GameState.GameOver:
                gameRunner.OnGameOver();
                return;
            case GameState.StageStart:
                gameRunner.OnStageStart();
                return;
            case GameState.StageOver:
                gameRunner.OnStageOver();
                return;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        if (Time.timeScale == 0 && gameState == GameState.Running)
        {
            Time.timeScale = 1;
        }
    }

    public void OnAttack(CharacterBase attacker, CharacterBase damageTaker)
    {
        if (damageTaker != null && attacker != null)
        {
            damageTaker.BeAttacked(attacker._damage);
        }
    }
    public void LoadDataGame() //Cong
    {
        gameData = dataGamePlay.LoadData();
    }
    public void SaveDataGame() //Cong
    {
        if (dataGamePlay != null)
        {
            dataGamePlay.SaveData(gameData);
        }
    }
    void SetPanelOptions() //Cong
    {
        if (gameData.hasBGM)
        {
            UIManager.Instance.panelOption.OnBGM();
        }
        else
        {
            UIManager.Instance.panelOption.OffBGM();
        }
        if (gameData.hasSFX)
        {
            UIManager.Instance.panelOption.OnSFX();
        }
        else
        {
            UIManager.Instance.panelOption.OffSFX();
        }

    }
}