using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator animator;
    string isRunHash = "isRun";
    string isDeadHash = "isDead";


    public void SetRun(bool isRunning)
    {
        animator.SetBool(isRunHash, isRunning);
    }

    public void SetDead()
    {
        animator.SetTrigger(isDeadHash);
    }
}
