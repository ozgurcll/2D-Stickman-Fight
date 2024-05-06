using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCBattleState : EnemyState
{
    private Transform player;
    private EnemyStickC enemy;
    public StickCBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickC _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        enemy.fx.CreateEmotesOfChance(1, enemy.transform);

        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        bool playerDetected = enemy.IsPlayerDetected();

        if (playerDetected && enemy.playerHit.distance < enemy.attackDistance)
        {
            stateTimer = enemy.battleTime;
            if (CanAttack())
                stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            stateMachine.ChangeState(enemy.seeTargetState);
        }

        if (enemy.CanBeHurt())
        {
            stateMachine.ChangeState(enemy.hurtState);
        }
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
