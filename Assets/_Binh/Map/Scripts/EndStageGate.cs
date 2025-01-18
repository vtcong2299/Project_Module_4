using UnityEngine;

public class EndStageGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.SetGameState(GameState.StageStart);
    }
}
