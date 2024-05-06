using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCDeadState : EnemyState
{
    private EnemyStickC enemy;
    public StickCDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickC _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();

        enemy.rb.gravityScale = 0;
        enemy.cc.enabled = false;
    }

    public override void Update()
    {
        base.Update();
    }
}
