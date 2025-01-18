using UnityEngine;

public class EndStageGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.SetGameState(GameState.StageStart);
    }
}
