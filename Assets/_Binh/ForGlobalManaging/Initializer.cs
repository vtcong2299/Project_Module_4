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

            gameElements.AddRange(obj.GetComponents<IOnGame>());
        }
    }

    void InvokeStarts()
    {
        statesRunner = new GameStatesContext(gameElements);
        statesRunner.OnGameStart(statesRunner);
        statesRunner.OnGameStart(enemyDieDependencies);
        statesRunner.OnGameStart(transformProvider);
        
    }
}
