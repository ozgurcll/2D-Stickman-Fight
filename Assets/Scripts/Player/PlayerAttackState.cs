using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int comboCount { get; private set; }

    private float lastTimeAttacked;
    private float comboWindow = 2;
    int randomSfx;
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        randomSfx = Random.Range(0, 2);
        AudioManager.instance.PlaySFX(randomSfx, null);
        xInput = 0;

        if (comboCount > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCount = 0;

        player.anim.SetInteger("ComboCounter", comboCount);

        if (player.haveSword)
            player.inventory.SwordDuration();

        stateTimer = .4f;
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(randomSfx);
        comboCount++;
        lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();



        if (stateTimer > 0)
            player.SetZeroVelocity();
        else
            stateMachine.ChangeState(player.idleState);

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
