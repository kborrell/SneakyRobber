﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    const string PLAYER = "Player";
    const string OBJECT = "Object";
    const float DISTANCE = 32;

    [SerializeField]
    private Transform flashlight;

    public enum GuardState
    {
        Idle,
        Patrol,
        Alert,
        Chase
    }

    [SerializeField]
    private GuardState currentState;
    private GuardState previousState;

    private IState activeState;
    Dictionary<GuardState, IState> guardStates = new Dictionary<GuardState, IState>();

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent
    {
        get { return navAgent; }
    }

    private Transform playerTransform;
    public Transform SeenPlayer
    {
        get { return playerTransform; }
    }

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        guardStates.Add(GuardState.Idle, new IdleState(this));
        guardStates.Add(GuardState.Patrol, new PatrolState(this));
        guardStates.Add(GuardState.Chase, new ChaseState(this));
    }

    private void Start()
    {
        StartCoroutine(FlashlightLookAnimation());
    }

    private void Update()
    {
        if(currentState != previousState)
        {
            ChangeState(currentState);
        }

        if (activeState != null)
        {
            activeState.Update();
        }
    }

    private void FixedUpdate()
    {
        bool inSigth = IsInSight(Vector3.forward);

        float margin = 0.5f;
        float steps = 5;
        float angle = margin / steps;

        for (int i = 0; i <= steps; i++)
        {
            inSigth |= IsInSight(new Vector3(-angle * i, 0, 1));
            inSigth |= IsInSight(new Vector3(angle * i, 0, 1));
        }

        if (inSigth && playerTransform != null)
        {
            flashlight.DOLookAt(playerTransform.position, 0.3f, AxisConstraint.Y);
            currentState = GuardState.Chase;
        }
        else
        {
            if(currentState != GuardState.Idle)
            {
                currentState = GuardState.Patrol;
            }
        }
    }

    private bool IsInSight(Vector3 angle)
    {
        Vector3 fwd = flashlight.TransformDirection(angle);

        fwd.y = 0;

        RaycastHit hit;
        Debug.DrawLine(flashlight.position - (flashlight.forward), flashlight.position + fwd * DISTANCE, Color.red);

        if (Physics.Raycast(flashlight.position, fwd, out hit, DISTANCE))
        {
            if (hit.collider.CompareTag(PLAYER))
            {
                playerTransform = hit.transform;
                return true;
            }

            if (hit.collider.CompareTag(OBJECT))
            {
                playerTransform = hit.transform;
                return true;
            }
        }

        return false;
    }

    private void ChangeState(GuardState newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }

        activeState = guardStates[newState];
        activeState.Enter();

        previousState = currentState;
    }

    IEnumerator FlashlightLookAnimation()
    {
        while (currentState == GuardState.Patrol)
        {
            float angle = (Random.value * 60) - 30;
            yield return flashlight.DOLocalRotate(new Vector3(5, angle, 0), 1f).SetEase(Ease.OutSine);
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }
}
