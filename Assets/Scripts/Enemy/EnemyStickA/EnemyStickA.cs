using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStickA : Enemy
{
    public StickAIdleState idleState { get; private set; }
    public StickAMoveState moveState { get; private set; }
    public StickASeeTargetState seeTargetState { get; private set; }
    public StickABattleState battleState { get; private set; }
    public StickAGunAttackState attackState { get; private set; }
    public StickAHurtState hurtState { get; private set; }
    public StickADeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new StickAIdleState(this, stateMachine, "Idle", this);
        moveState = new StickAMoveState(this, stateMachine, "Walk", this);
        seeTargetState = new StickASeeTargetState(this, stateMachine, "Move", this);
        battleState = new StickABattleState(this, stateMachine, "Idle", this);
        attackState = new StickAGunAttackState(this, stateMachine, "Attack", this);
        hurtState = new StickAHurtState(this, stateMachine, "Hurt", this);
        deadState = new StickADeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeHurt()
    {
        if (base.CanBeHurt())
        {
            stateMachine.ChangeState(hurtState);
            return true;
        }
        return false;
    }


    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
