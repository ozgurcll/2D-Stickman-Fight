using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttack : PlayerState
{
    int x = 0;
    public PlayerCounterAttack(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.attackCooldown = .9f;
        stateTimer = .2f;
        player.anim.SetBool("SuccessfullCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
        x = 0;
    }
    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<BulletController>() != null)
            {
                hit.GetComponent<BulletController>().FlipBullet();
                SuccesfulCounterAttack();
                if (x == 0)
                    player.fx.CreateCriticalAttack();
                x = 1;
            }


            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeHurt())
                {
                    player.DamageImpact();
                    SuccesfulCounterAttack();
                }
            }
        }


        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    private void SuccesfulCounterAttack()
    {
        stateTimer = 10; // any value bigger than 1
        player.anim.SetBool("SuccessfullCounterAttack", true);
    }
}
