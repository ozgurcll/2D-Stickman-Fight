using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAGunAttackState : EnemyState
{
    private Transform player;
    EnemyStickA enemy;
    public StickAGunAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickA _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

        enemy.anim.SetInteger("ComboCounter", 0);

        stateTimer = .3f;

        if (!player.GetComponent<PlayerStats>().isDead)
            enemy.fx.CreateEmotesOfChance(0, enemy.transform);


        enemy.fx.CreateBullet();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

}
