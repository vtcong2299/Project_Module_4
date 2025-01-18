using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IOnGameStates
{
    GameState gameState;
    List<IOnGameStates> gameElements;
    [SerializeField]
    Initializer initializer;
    public DataGamePlay dataGamePlay;
    public PlayerData playerData;

    private void Start()
    {
        dataGamePlay.StartDataGamePlay();
        LoadDataGame();
        SetPanelOptions();
        initializer.InjectAllAtGameStart();
        SetGameState(GameState.StageStart);
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.None:
                return;
            case GameState.Running:
                Iterate(gameElements, instance => instance.OnGameRunning());
                return;
            case GameState.Pause:
                Iterate(gameElements, instance => instance.OnGamePause());
                return;
            case GameState.GameOver:
                Iterate(gameElements, instance => instance.OnGameOver());
                return;
            case GameState.StageStart:
                Iterate(gameElements, instance => instance.OnStageStart());
                return;
            case GameState.StageOver:
                Iterate(gameElements, instance => instance.OnStageOver());
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

    void Iterate<T>(List<T> instances, Action<T> Invoke)
    {
        foreach (T instance in instances)
        {
            Invoke(instance);
        }
    }

    public void OnGameStart(params object[] parameter)
    {
        foreach (object obj in parameter)
        {
            if (obj is List<IOnGameStates> param)
            {
                gameElements = param;
            }
        }
    }

    public void OnGamePause()
    {
        Time.timeScale = 0;
        SetGameState(GameState.None);
    }

    public void OnGameOver()
    {
        SetGameState(GameState.None);
    }

    public void OnStageStart()
    {
        SetGameState(GameState.Running);
    }

    public void OnStageOver()
    {
        SetGameState(GameState.Running);
    }

    public void OnAttack(CharacterBase attacker, CharacterBase damageTaker)
    {
        if (damageTaker != null && attacker != null)
        {
            damageTaker.BeAttacked(attacker._damage);
        }
    }
    public void LoadDataGame()
    {
        playerData = dataGamePlay.LoadData();
    }
    public void SaveDataGame()
    {
        if (dataGamePlay != null)
        {
            dataGamePlay.SaveData(playerData);
        }
    }
    void SetPanelOptions()
    {
        if (playerData.hasBGM)
        {
            UIManager.Instance.panelOption.OnBGM();
        }
        else
        {
            UIManager.Instance.panelOption.OffBGM();
        }
        if (playerData.hasSFX)
        {
            UIManager.Instance.panelOption.OnSFX();
        }
        else
        {
            UIManager.Instance.panelOption.OffSFX();
        }

    }
}