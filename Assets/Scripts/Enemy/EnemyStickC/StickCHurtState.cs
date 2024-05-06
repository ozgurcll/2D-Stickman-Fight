using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCHurtState : EnemyState
{
    Player player;
    private EnemyStickC enemy;
    private int x = 0;
    private bool isAngry;
    private int hurtCombo;
    public StickCHurtState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickC _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.CreateEmotesOfChance(1, enemy.transform);
        player = PlayerManager.instance.player;
        stateTimer = 1;
        enemy.fx.StartCoroutine("FlashFX");
        rb.velocity = new Vector2(-enemy.facingDir * 10, 12);
    }
    public override void Exit()
    {
        base.Exit();
        x = 0;
    }
    public override void Update()
    {
        base.Update();
        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        if (distance < 1.75f && player.haveSword && Input.GetKeyDown(KeyCode.F) && x == 0)
        {

            player.stateMachine.ChangeState(player.attackState);
            player.anim.SetInteger("ComboCounter", 2);
            enemy.StartCoroutine(knockbackDelay());
            enemy.fx.CreateCriticalAttack();
            if (hurtCombo == 3)
            {
                enemy.fx.CreateEmotes(0, enemy.transform);
                enemy.stats.damage.AddModifier(10);
                enemy.moveSpeed = 5.6f;
            }
        }


        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);

    }

    IEnumerator knockbackDelay()
    {
        hurtCombo++;
        x = 1;
        enemy.fx.CreateEmotesOfChance(2, enemy.transform);
        yield return new WaitForSeconds(.15f);
        rb.velocity = new Vector2(-enemy.facingDir * 21, 7);
    }
}
