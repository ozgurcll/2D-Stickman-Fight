using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool catidanInme;
    public LayerMask whatIsFloorGround;
    private bool isDie;


    public bool haveGun;
    public bool haveSword;
    public float attackCooldown;
    public float attackCooldownTime;
    public EntityFX fx { get; private set; }
    public Inventory inventory { get; private set; }

    [Header("")]
    public Transform InteractableCheck;
    public float interactableRadius;
    public LayerMask whatIsInteractable;


    [Header("Player Variables")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Player Slide Variables")]
    public float slideSpeed;
    public float slideDuration;
    private float defaultSlideSpeed;
    public float slideDir { get; private set; }

    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlide wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerCounterAttack counterAttackState { get; private set; }
    public PlayerGunAttackState gunAttackState { get; private set; }
    public PlayerMoveShot moveShotState { get; private set; }
    public PlayerClimbState climbState { get; private set; }
    public PlayerDropState dropState { get; private set; }
    public PlayerSlideState slideState { get; private set; }
    public PlayerDieState dieState { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlide(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttack(this, stateMachine, "CounterAttack");
        gunAttackState = new PlayerGunAttackState(this, stateMachine, "Attack");
        moveShotState = new PlayerMoveShot(this, stateMachine, "Move");
        climbState = new PlayerClimbState(this, stateMachine, "Climb");
        dropState = new PlayerDropState(this, stateMachine, "Jump");
        slideState = new PlayerSlideState(this, stateMachine, "Slide");
        dieState = new PlayerDieState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();
        fx = GetComponent<EntityFX>();
        inventory = Inventory.instance;
        stateMachine.Initialize(idleState);

        defaultSlideSpeed = slideSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        attackCooldown -= Time.deltaTime;
        SlideInput();
    }

    private void SlideInput()
    {
        if (IsWallDetected() || !IsGroundDetected() || isDie)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            slideDir = Input.GetAxisRaw("Horizontal");

            if (slideDir == 0)
                slideDir = facingDir;


            stateMachine.ChangeState(slideState);
        }
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public bool IsInteractableDetected() => Physics2D.OverlapCircle(InteractableCheck.position, interactableRadius, whatIsInteractable);
    public bool IsFloorGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsFloorGround);
    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(InteractableCheck.position, interactableRadius);
    }

    public override void Die()
    {
        base.Die();
        isDie = true;
        stateMachine.ChangeState(dieState);
    }


    public void AttackInput()
    {
        if (haveGun)
        {
            if (attackCooldown <= 0)
                stateMachine.ChangeState(gunAttackState);
        }
        else
            stateMachine.ChangeState(attackState);
    }
    public void DodgeInput()
    {
        if (haveSword && attackCooldown <= 0)
            stateMachine.ChangeState(counterAttackState);
    }
}
