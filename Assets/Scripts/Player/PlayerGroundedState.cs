using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.IsInteractableDetected() && Input.GetKeyDown(KeyCode.W))
            stateMachine.ChangeState(player.climbState);

        if (Input.GetKey(KeyCode.S) && player.IsFloorGroundDetected())
        {
            player.catidanInme = true;
            if (Input.GetKeyDown(KeyCode.Space))
                stateMachine.ChangeState(player.dropState);
        }
        else
            player.catidanInme = false;

        if (Input.GetKeyDown(KeyCode.Q) && player.haveSword && player.attackCooldown <= 0)
            stateMachine.ChangeState(player.counterAttackState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (player.haveGun)
            {
                if (xInput != 0 && player.attackCooldown <= 0)
                    stateMachine.ChangeState(player.moveShotState);
                else if (xInput == 0 && player.attackCooldown <= 0)
                    stateMachine.ChangeState(player.gunAttackState);
            }
            else
                stateMachine.ChangeState(player.attackState);
        }

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected() && !player.catidanInme)
            stateMachine.ChangeState(player.jumpState);


    }
}
