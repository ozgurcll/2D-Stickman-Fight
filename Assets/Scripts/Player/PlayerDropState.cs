using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropState : PlayerState
{
    public PlayerDropState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = .3f;
        player.cc.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.cc.enabled = true;
        player.catidanInme = false;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
    }
}
