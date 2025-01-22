using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamSetterOnState : StateMachineBehaviour
{
    [SerializeField]
    string theParam;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(theParam, true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(theParam, false);
    }
}
