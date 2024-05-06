using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDrop;
    public int myScore;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        myDrop = GetComponent<ItemDrop>();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();

        enemy.Die();
        GameManager.instance.score += myScore;
        myDrop.GenerateDrop();
        Destroy(gameObject, 2f);
    }
}