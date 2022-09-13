using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphere : EnemyAI
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Avatar>().TakeDamage(10);
        }
    }
}
