using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickADeadState : EnemyState
{
    protected EnemyStickA enemy;
    public StickADeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickA _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();

        enemy.rb.gravityScale = 0;
        enemy.cc.enabled = false;
    }

}
