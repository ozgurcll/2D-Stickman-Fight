using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCAttackState : EnemyState
{
    private EnemyStickC enemy;

    public int comboCount { get; private set; }
    public StickCAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickC _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCount > 2)
            comboCount = 0;

        enemy.anim.SetInteger("ComboCounter", comboCount);
    }

    public override void Exit()
    {
        base.Exit();
        comboCount++;
        enemy.lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
