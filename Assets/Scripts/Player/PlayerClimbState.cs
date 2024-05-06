using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerState
{
    public PlayerClimbState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cc.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.cc.enabled = true;
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.W) && player.IsInteractableDetected())
            player.SetVelocity(0, 3);
        else if (Input.GetKeyUp(KeyCode.W))
            stateMachine.ChangeState(player.airState);
        else if (!player.IsInteractableDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
