using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerBehaviour : StateMachineBehaviour
{
    readonly string spawnParam = "isSpawning";
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(spawnParam, true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(spawnParam, false);
    }
}
