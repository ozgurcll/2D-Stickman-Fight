using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveShot : PlayerState
{
    public PlayerMoveShot(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(6, null);
        player.attackCooldown = player.attackCooldownTime;
        player.inventory.AmmoDuration();
        player.fx.CreateBullet();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (xInput != 0)
            stateMachine.ChangeState(player.moveState);
        else
            stateMachine.ChangeState(player.idleState);
    }
}