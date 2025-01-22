using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Animator animator;
    readonly string idleState = "EllenIdle";
    readonly string attackState = "EllenAttack";
    readonly float transitionTime = 0.2f;

    Quaternion angleSave;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttackEnd()
    {
        animator.CrossFade(idleState, transitionTime);
        transform.rotation = angleSave;
    }

    public void OnIleMiddle()
    {
        if (target.position.y < 0)
        {
            return;
        }
        angleSave = transform.rotation;
        Vector3 direction = target.position - transform.position;
        direction.y = transform.position.y;
        transform.forward = direction.normalized;
        animator.Play(attackState);
    }
}
