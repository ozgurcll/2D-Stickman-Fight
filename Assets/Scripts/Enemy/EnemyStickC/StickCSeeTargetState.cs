using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCSeeTargetState : EnemyState
{
    private Transform player;
    private int moveDir;

    private EnemyStickC enemy;
    public StickCSeeTargetState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyStickC _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.moveSpeed = 5;
        player = PlayerManager.instance.player.transform;
        stateTimer = enemy.battleTime;
        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
        else
            enemy.fx.CreateEmotesOfChance(2, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        bool playerDetected = enemy.IsPlayerDetected();

        if (playerDetected)
        {
            // Player tespit edildiyse ve attackDistance içindeyse savaş durumuna geç
            if (enemy.playerHit.distance < enemy.attackDistance)
            {
                stateMachine.ChangeState(enemy.battleState);
                return; // Savaş durumuna geçtiğimizde diğer koşullara bakmaya gerek yok
            }
        }
        else if (stateTimer < 0 || Vector3.Distance(player.transform.position, enemy.transform.position) > 10)
        {
            // Player tespit edildiyse ve attackDistance dışındaysa ve stateTimer sıfırdan küçükse veya player ile enemy arasındaki mesafe 10 birimden uzaksa idle durumuna geç
            stateMachine.ChangeState(enemy.idleState);
            return; // Idle durumuna geçtiğimizde diğer koşullara bakmaya gerek yok
        }

        // Player tespit edilmediyse veya attackDistance içinde değilse ve savaş durumuna geçmek için gerekli mesafede değilse, hareketi devam ettir
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        // Eğer düşman attackDistance'dan hafif bir mesafede ise, hareketi devam ettirme
        if (playerDetected && enemy.playerHit.distance < enemy.attackDistance - 0.1f)
            return;
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);

    }
}
