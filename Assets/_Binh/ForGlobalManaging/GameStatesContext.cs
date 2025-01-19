using System;
using System.Collections.Generic;

public class GameStatesContext : IOnGameStates
{
    List<IOnGame> onGameElements;

    List<IOnStageOver> onStageOverElements;
    List<IOnStageStart> onStageStartElements;
    List<IOnGameOver> onGameOverElements;
    List<IOnGamePause> onGamePauseElements;
    List<IOnGameRunning> onGameRunningElements;

    public GameStatesContext(List<IOnGame> param)
    {
        onGameElements = param;
    }

    public void OnGameOver()
    {
        Iterate(onGameOverElements, element => element.onGameOverAction());
    }

    public void OnGamePause()
    {
        Iterate(onGamePauseElements, element => element.onGamePauseAction());
    }

    public void OnGameRunning()
    {
        Iterate(onGameRunningElements, element => element.onGameRunningAction());
    }

    public void OnGameStart<T>(T parameter)
    {
        Iterate(onGameElements, element =>
        {
            if (element is IOnGameStart<T> instant)
            {
                instant.onGameStartAction(parameter);
            }
        });
    }

    public void OnStageOver()
    {
        Iterate(onStageOverElements, element => element.onStageOverAction());
    }

    public void OnStageStart()
    {
        Iterate(onStageStartElements, element => element.onStageStartAction());
    }

    void Iterate<T>(List<T> list, Action<T> Invoke)
    {
        if (list != null)
        {
            if (list.Count == 0)
            {
                return;
            }
            foreach (T element in list)
            {
                Invoke(element);
            }
            return;
        }
        else
        {
            list = new List<T>();
        }
        foreach (IOnGame element in onGameElements)
        {
            if (element is T instant)
            {
                Invoke(instant);
                list.Add(instant);
            }
        }
    }

}
