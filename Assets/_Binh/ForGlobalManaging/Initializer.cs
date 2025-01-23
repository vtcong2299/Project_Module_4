using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Initializer
{
    List<IOnGame> gameElements;
    [SerializeField]
    GameObject[] gameElementObjects;
    List<IOnEnemyDie> enemyDieDependencies;
    ITransformGettable transformProvider;

    IOnGameStates statesRunner;
    IGameData gameData;
    IDataManipulator dataManipulator;
    IRespawnable respawner;
    ISpawnable spawner;

    public void InjectAllAtGameStart()
    {
        gameElements = new List<IOnGame>();
        enemyDieDependencies = new List<IOnEnemyDie>();
        Init();
        InvokeStarts();
    }

    void Init()
    {
        foreach (GameObject obj in gameElementObjects)
        {
            if (obj.TryGetComponent(out ITransformGettable iTransformGettable))
            {
                transformProvider = iTransformGettable;
            }
            if (obj.TryGetComponent(out IOnEnemyDie iOnEnemyDie))
            {
                enemyDieDependencies.Add(iOnEnemyDie);
            }
            if (obj.TryGetComponent(out IGameData iGameData))
            {
                gameData = iGameData;
            }
            if (obj.TryGetComponent(out IDataManipulator iDataManipulator))
            {
                dataManipulator = iDataManipulator;
            }
            if (obj.TryGetComponent(out IRespawnable iRespawnable))
            {
                respawner = iRespawnable;
            }
            if (obj.TryGetComponent(out ISpawnable iSpawnable))
            {
                spawner = iSpawnable;
            }

            gameElements.AddRange(obj.GetComponents<IOnGame>());
        }
    }

    void InvokeStarts()
    {
        statesRunner = new GameStatesContext(gameElements);
        statesRunner.OnGameStart(statesRunner);
        statesRunner.OnGameStart(enemyDieDependencies);
        statesRunner.OnGameStart(transformProvider);
        statesRunner.OnGameStart(respawner);
        statesRunner.OnGameStart(spawner);
        DataGamePlay.Instance.StartDataGamePlay();
        statesRunner.OnGameStart(gameData);
        statesRunner.OnGameStart(dataManipulator);
        DataGameStartSetUp dataAtStart = new DataGameStartSetUp(gameData);
        dataAtStart.SetUp();
    }
}
