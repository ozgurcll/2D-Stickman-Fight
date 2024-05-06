using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    damage,
    critChance,
    critPower,
    health
}

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    public Stat damage;
    public Stat critChance;
    public Stat critPower;
    public Stat maxHealth;

    public float currentHealth;
    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }

    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        currentHealth = GetMaxHealthValue();
        critPower.SetDefaultValue(150);
    }

    protected virtual void Update()
    {

    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealthBy(_damage);


        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");
        if (currentHealth <= GetMaxHealthValue() / 2)
        {
            if (fx.yarabandi != null)
                fx.yarabandi.SetActive(true);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }

    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        bool criticalStrike = false;

        if (_targetStats.isInvincible)
            return;

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
            criticalStrike = true;
        }

        fx.CreateHitFX(_targetStats.transform, criticalStrike);

        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    public virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (_damage > 0)
            fx.CreatePopUpText(_damage.ToString());

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }


        return false;
    }

    public void KillEntity()
    {
        if (!isDead)
            Die();
    }


    protected int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = critPower.GetValue() * .01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;

    protected virtual void Die()
    {
        isDead = true;
    }
}
