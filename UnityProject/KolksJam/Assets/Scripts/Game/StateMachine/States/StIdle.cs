using System;
using UnityEngine;
using Prime31.StateKit;

public class StIdle : SKState<EnemyController>
{
    public override void begin()
    {
        base.begin();
        Context.Animator.SetFloat("Speed", 0);
        Context.Animator.SetBool("Attack", false);
    }
    public override void update(float deltaTime)
    {
        
    }
}
