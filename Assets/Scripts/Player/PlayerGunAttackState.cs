using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunAttackState : PlayerState
{
    public PlayerGunAttackState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }
    override public void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(6, null);
        player.anim.SetInteger("ComboCounter", 0);
        player.attackCooldown = player.attackCooldownTime;
        stateTimer = .3f;

        player.inventory.AmmoDuration();
        player.fx.CreateBullet();
    }
    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(6);
    }
    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
