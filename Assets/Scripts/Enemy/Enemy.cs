using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    public RaycastHit2D playerHit;

    [Header("Hurt State")]
    public float hurtDuration;
    public Vector2 hurtDirection = new Vector2(10, 12);
    protected bool canBeHurt;
    [SerializeField] protected GameObject counterImage;

    [Header("Movement State")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    [HideInInspector] public float defaultMoveSpeed;


    [Header("Attack State")]
    public Transform attackTriggered;
    public float attackTriggeredDistance;
    [Header("")]
    public float agroDistance;
    public float attackDistance;
    public float attackCooldown;
    [Header("")]
    public float minAttackCooldown;
    public float maxAttackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }
    public EntityFX fx { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }
    protected override void Start()
    {
        base.Start();
        fx = GetComponent<EntityFX>();
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    #region Counter Attack Window
    public virtual void OpenCounterAttackWindow()
    {
        canBeHurt = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeHurt = false;
        counterImage.SetActive(false);
    }
    #endregion

    public virtual bool CanBeHurt()
    {
        if (canBeHurt)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }


    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual void AssignLastAnimName(string _animBoolName) => lastAnimBoolName = _animBoolName;

    //public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(attackTriggered.position, Vector2.right * facingDir, attackTriggeredDistance, whatIsPlayer);

    public virtual bool IsPlayerDetected()
    {
        playerHit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, attackTriggeredDistance, whatIsPlayer);
        return playerHit.collider != null;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawLine(attackTriggered.position, new Vector3(attackTriggered.position.x + attackDistance * facingDir, attackTriggered.position.y));
        Gizmos.DrawLine(attackTriggered.position, new Vector2(attackTriggered.position.x + attackTriggeredDistance * facingDir, attackTriggered.position.y));
        if (IsPlayerDetected())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(attackTriggered.position, new Vector2(attackTriggered.position.x + attackTriggeredDistance * facingDir, attackTriggered.position.y));

        }
    }

}
