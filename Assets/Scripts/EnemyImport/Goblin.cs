using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyAI
{
    //Unused but taken from FPS project as a basis

    //Called when the script instance is being loaded
    protected override void Awake()
    {
        
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        _navAgent.SetDestination(Target.transform.position);
    }

    public override void StartIdle()
    {
        _currentState = AIStates.Idle;

        _navAgent.enabled = false;
    }

    protected override void Idle()
    {

    }

   /* protected override void StartDead()
    {
        _currentState = AIStates.Dead;

        //AiManager.HideEnemy(gameObject);
    }

    protected override void Dead()
    {
        StartIdle();
    }*/
}
