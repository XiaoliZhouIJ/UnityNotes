using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;

    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        // Debug.Log("State React");

        parameter.animator.Play("EnemyReact");
    }
    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);


        Debug.Log(info.normalizedTime);


        if (info.normalizedTime >= 0.95F)
        {            
            manager.TransitionState(StateType.Chase);
        }
    }
    public void OnExit()
    {
        Debug.Log("React Over");
    }
}
