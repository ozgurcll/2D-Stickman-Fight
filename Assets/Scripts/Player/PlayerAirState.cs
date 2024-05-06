using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0) // KARAKTERİMİZ HAVADAYKEN YÖN TUŞLARINA BASILIYORSA HARAKETİ NORMAL HIZDA YAPAR AMA BU SATIR HAVADA HARAKETİ YAVAŞLATIR VE DOĞRU BİR OYNANIŞ SAĞLAR
            player.rb.velocity = new Vector2(player.moveSpeed * .8f * xInput, player.rb.velocity.y);
    }
}
