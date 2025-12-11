using Godot;


public partial class DoubleJumpingMovementState : State
{
    private MainCharacter _player;

    public override async void Ready()
    {
        _player = (MainCharacter)GetTree().GetFirstNodeInGroup("MainCharacterGroup");
        if (!_player.IsNodeReady())
            await ToSignal(_player, "ready");
    }
    public override void Enter()
    {
        _player.EmitSignal("Jumped");
        _player.SetAnimation("double");
    }
    public override void Update(double delta)
    {
       if (!_player.IsOnFloor() && _player.Velocity.Y >= 0)
        {
                stateMachine.TransitionTo("FallingMovementState");
        }

    }

    public override void UpdatePhysics(double delta)
    {
        Godot.Vector2 velocity = _player.Velocity;
        float direction = Input.GetAxis("move_left", "move_right");

        if (!_player.IsOnFloor())
        {
            velocity += _player.GetGravity() * (float)delta;

             if (direction!=0.0f)
            {
                velocity.X = direction * _player.GetSpeed();
            }

        }

		if (Input.IsActionJustPressed("ui_accept")&&_player.GetDoubleJump())
		{
			velocity.Y = _player.GetJumpVelocity();

		}

        _player.setdoubleJump(false);
        _player.Velocity = velocity;
        _player.MoveAndSlide();

    }
}
