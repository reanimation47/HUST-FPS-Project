using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Patrol : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;
    public int waypointIndex;
    public EnemyState_Patrol(EnemyReferences enemyReferences) {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter()
    {
        target = GameObject.FindWithTag("Player").transform;
        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    public void OnExit()
    {
        enemyReferences.animator.SetFloat("speed", 0f);
    }

    public void Tick()
    {
        //Patrol Cycle
        if (enemyReferences.Agent.remainingDistance < 0.2f)
        {
            if (waypointIndex < enemyReferences.brain.path.waypoints.Count - 1) {
                waypointIndex++;
            }
            else waypointIndex = 0;
            enemyReferences.Agent.SetDestination(enemyReferences.brain.path.waypoints[waypointIndex].position);
            enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
        }
    }

    public bool HasDetectedPlayer() {
        float distanceToTarget = Vector3.Distance(enemyReferences.transform.position, target.position);
        return distanceToTarget <= enemyReferences.detectPlayerRange;
    }

    public Color GizmoColor()
    {
        return Color.gray;
    }
}
