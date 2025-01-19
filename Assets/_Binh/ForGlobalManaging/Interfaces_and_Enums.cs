using System;
using UnityEngine;

#region Interfaces
public interface IAttackable
{
    int _damage { get; }
}

public interface IBeAttackedable
{
    void BeAttacked(int damage);
}

public interface IOnEnemyDie
{
    void OnEnemyDie();
}

public interface ITransformGettable
{
    Transform _transform { get; }
}

public interface IMapGenerattable
{
    void GenerateUnits(Func<Vector3> PivotSetter = null);
}

public interface IOnGameStates
{
    void OnGameStart<T>(T parameter);
    void OnGameRunning();
    void OnGamePause();
    void OnStageStart();
    void OnStageOver();
    void OnGameOver();
}

public interface IOnGame
{ }

public interface IOnGameStart<T> : IOnGame
{
    Action<T> onGameStartAction { get; }
}

public interface IOnGamePause : IOnGame
{
    Action onGamePauseAction { get; }
}

public interface IOnGameRunning : IOnGame
{
    Action onGameRunningAction { get; }
}

public interface IOnStageStart : IOnGame
{
    Action onStageStartAction { get; }
}

public interface IOnStageOver : IOnGame
{
    Action onStageOverAction { get; }
}

public interface IOnGameOver : IOnGame
{
    Action onGameOverAction { get; }
}

#endregion
#region Enums
public enum GameState
{
    None,
    Running,
    Pause,
    StageStart,
    StageOver,
    GameOver
}
#endregion