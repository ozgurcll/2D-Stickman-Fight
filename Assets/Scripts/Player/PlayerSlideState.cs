using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerState
{
    public PlayerSlideState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(5, null);
        stateTimer = player.slideDuration;
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(5);
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        player.SetVelocity(player.slideSpeed * player.slideDir, rb.velocity.y);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
