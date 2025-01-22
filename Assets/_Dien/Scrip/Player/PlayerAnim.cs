using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator animator;
    string isRunHash = "isRun";
    string isDeadHash = "isDead";
    string isSpawnHash = "isSpawning";
    string isAttackHash = "isAttacking";   
    string isIdleHash = "isIdling";
    string attackStateName = "EllenAttack";


    public void SetRun(bool isRunning)
    {
        animator.SetBool(isRunHash, isRunning);
    }

    public void SetDead()
    {
        animator.SetTrigger(isDeadHash);
    }

    public bool IsSpawning()
    {
        return animator.GetBool(isSpawnHash);
    }

    public bool IsAttacking()
    {
        return animator.GetBool(isAttackHash);
    }

    public bool IsIdling()
    {
        return animator.GetBool(isIdleHash);
    }

    public void TriggerAttack()
    {
        animator.Play(attackStateName);
    }
}
