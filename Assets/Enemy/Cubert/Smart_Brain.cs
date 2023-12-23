using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart_Brain : MonoBehaviour
{
    
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;
    public EnemyPath path;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();

        stateMachine = new StateMachine();

        CoverArea coverArea = FindObjectOfType<CoverArea>();

        //STATES
        var runToCover = new EnemyState_RunToCover(enemyReferences, coverArea);
        var delayAfterRun = new EnemyState_Delay(2f);
        var cover = new EnemyState_Cover(enemyReferences);
        var patrol = new EnemyState_Patrol(enemyReferences);

        //TRANSITIONS
        At(patrol, runToCover, () => patrol.HasDetectedPlayer());
        At(runToCover, delayAfterRun, () => runToCover.HasArrivedAtDestination());
        At(delayAfterRun, cover, () => delayAfterRun.IsDone());
        At(cover, patrol, () => cover.IsPlayerOutOfRange());



        // START STATE
        stateMachine.SetState(patrol);

        //FUNCTIONS & CONDITIONS
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);


    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Tick();
    }

    private void OnDrawGizmos() {
        if (stateMachine != null) {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }    
    }
    
}
