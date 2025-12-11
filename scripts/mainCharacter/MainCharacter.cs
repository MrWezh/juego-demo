using Godot;


public partial class MainCharacter : CharacterBody2D
{
	private const float Speed = 200.0f;
	private const float JumpVelocity = -400.0f;

	private bool doubleJump = true;

    private StateMachine stateMachine;

    [Signal] public delegate void JumpedEventHandler();
    public override void _Ready()
    {
         stateMachine = GetNode<StateMachine>("MovementStateMachine");
    }
	public override void _PhysicsProcess(double delta)
	{
	}

    public override void _Process(double delta)
    {
        AnimatedSprite2D sprite = GetNode<AnimatedSprite2D> ("Sprite");

		if (Velocity.X < 0.0f)
        {
            sprite.FlipH = true;
        }
		else if (Velocity.X > 0.0f)
			sprite.FlipH = false;			

    }

	public float GetSpeed()
    {
        return Speed;
    }

	public float GetJumpVelocity()
    {
        return JumpVelocity;
    }

	public bool GetDoubleJump()
    {
        return doubleJump;
    }

	public void setdoubleJump(bool x)
    {
        this.doubleJump = x;


    }

    public void Kill()
    {
        stateMachine.TransitionTo("DieMovementState");
    }
	public void SetAnimation(string animation)
    {
		AnimatedSprite2D sprite = GetNode<AnimatedSprite2D> ("Sprite");
		GD.Print($"Playing: {animation}");
		sprite.Play(animation);

    }
}
