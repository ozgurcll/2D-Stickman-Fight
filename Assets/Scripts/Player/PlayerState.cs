using UnityEngine;

public class PlayerState
{

    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    public float xInput;
    protected float yInput;
    protected float stateTimer;

    private string animationBoolName;
    protected bool triggerCalled;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animationBoolName = _animationBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animationBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        Debug.Log(player.stateMachine.currentState);
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animationBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
