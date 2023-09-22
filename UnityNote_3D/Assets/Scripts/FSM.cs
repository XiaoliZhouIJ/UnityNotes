using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Patrol,
    Chase,
    React,
    Attack
}

[Serializable]
public class Parameter
{
    public int health;
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;

    public Transform[] patrolPoints;            // Ѳ�߷�Χ
    public Transform[] chasePoints;             // ׷����Χ
    
    public Transform target;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackArea;
    
    public Animator animator;

   
}

public class FSM : MonoBehaviour
{
    public Parameter parameter;

    private IState currentState;

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    private void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));

        TransitionState(StateType.Idle);

        // parameter.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    public void TransitionState(StateType state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[state];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
        // Debug.Log(target.position);
        if (target != null)
        {
            // Debug.Log(transform.position.x + "/t" + target.position.x);
            if(transform.position .x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    }
}
