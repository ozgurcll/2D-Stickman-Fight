using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(3,null);
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(3);
    }
    public override void Update()
    {
        base.Update();
        if (player.transform.rotation.y < 180)
            player.FlipController(xInput);
        else
            player.FlipController(-xInput);


        if (player.IsInteractableDetected() && Input.GetKeyDown(KeyCode.W))
            stateMachine.ChangeState(player.climbState);

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //  stateMachine.ChangeState(player.attackState);
        if (xInput != 0) //Slide ile birlikte çalıştığı zaman hoş bir oynanış sağlamıyor belki ilerde bu satır silinir slidedan zıplamaya geçişte hız yavaşlamasın
            rb.velocity = new Vector2(player.moveSpeed * xInput, rb.velocity.y);//AİR STATE İÇİNDEDE VAR BU KOD


        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}
