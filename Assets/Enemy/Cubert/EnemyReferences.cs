using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyShooter shooter;
    [HideInInspector] public Smart_Brain brain;
    

    [Header("Stats")]

    public float pathUpdateDelay = 0.2f;
    public float detectPlayerRange = 10f;


    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shooter = GetComponent<EnemyShooter>();
        brain = GetComponent<Smart_Brain>();
    }


    
}
