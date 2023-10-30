using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Shoot : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;

    public EnemyState_Shoot(EnemyReferences enemyReferences) {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter() {
        Debug.Log("Start Shooting");
        target = GameObject.FindWithTag("Player").transform;
    }

    public void OnExit() {
        Debug.Log("Stop Shooting");
        enemyReferences.animator.SetBool("shooting", false);
        target = null;
    }

    public void Tick() {
        if (target != null) {
            // Aim at the target
            Vector3 lookPos = target.position - enemyReferences.transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, 0.2f);
        }

        // Decide to shoot or hide. For now, shoot first.
        enemyReferences.animator.SetBool("shooting", true);
    }

    public Color GizmoColor() {
        return Color.red;
    }
}
