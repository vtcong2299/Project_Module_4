using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IOnGameOver, IOnGamePause, IOnGameStart<IOnGameStates>, IOnStageOver, IOnStageStart, IOnGameRunning
{
    GameState gameState;
    IOnGameStates gameRunner;
    [SerializeField]
    Initializer initializer;

    public Action onGameOverAction => () => SetGameState(GameState.None);

    public Action onGamePauseAction => () =>
    {
        Time.timeScale = 0;
        SetGameState(GameState.None);
    };

    public Action onStageOverAction => () => SetGameState(GameState.Running);

    public Action onStageStartAction => () => SetGameState(GameState.Running);

    public Action<IOnGameStates> onGameStartAction => param => gameRunner = param;

    public Action onGameRunningAction => () =>
    {
        Time.timeScale = 1;
        SetGameState(GameState.None);
    };

    private void Start()
    {
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
    }

    public void OnAttack(CharacterBase attacker, CharacterBase damageTaker)
    {
        if (damageTaker != null && attacker != null)
        {
            damageTaker.BeAttacked(attacker._damage);
        }
    }

}