using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStickC : Enemy
{
    public StickCIdleState idleState { get; private set; }
    public StickCMoveState moveState { get; private set; }
    public StickCSeeTargetState seeTargetState { get; private set; }
    public StickCBattleState battleState { get; private set; }
    public StickCAttackState attackState { get; private set; }
    public StickCHurtState hurtState { get; private set; }
    public StickCDeadState deadState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new StickCIdleState(this, stateMachine, "Idle", this);
        moveState = new StickCMoveState(this, stateMachine, "Walk", this);
        seeTargetState = new StickCSeeTargetState(this, stateMachine, "Move", this);
        battleState = new StickCBattleState(this, stateMachine, "Idle", this);
        attackState = new StickCAttackState(this, stateMachine, "Attack", this);
        hurtState = new StickCHurtState(this, stateMachine, "Hurt", this);
        deadState = new StickCDeadState(this, stateMachine, "Die", this);
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
