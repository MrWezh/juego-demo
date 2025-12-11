using Godot;
using GodotPlugins.Game;
using System;

public partial class RunningMovementState : State
{
    private MainCharacter _player;
    private Timer coyoteTimer;

    private bool canJump = true;

    public override async void Ready()
    {
        _player = (MainCharacter)GetTree().GetFirstNodeInGroup("MainCharacterGroup");
        coyoteTimer = GetNode<Timer> ("CoyoteTimer");
        if (!_player.IsNodeReady())
            await ToSignal(_player, "ready");
    }
    public override void Enter()
    {
        _player.SetAnimation("run");
    }

    public override void Update(double delta)
	{

		if (!_player.IsOnFloor())
		{
			if (canJump)
            {
                coyoteTimer.Start();
                canJump = false;
            }
         else if (coyoteTimer.IsStopped()) {
                stateMachine.TransitionTo("FallingMovementState");
            }
		}
		else if(_player.IsOnFloor())
		{
            canJump = true;
            coyoteTimer.Stop();
            _player.setdoubleJump(true);
			if (_player.Velocity.Y == 0 && _player.Velocity.X == 0)
				stateMachine.TransitionTo("IdleMovementState");
		}
		
    }

    public override void UpdatePhysics(double delta)
    {
        Vector2 velocity = _player.Velocity;
        
        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0.0f)
        {
            velocity.X = direction * _player.GetSpeed();
        }
        else
		{
			velocity.X = Mathf.MoveToward(_player.Velocity.X, 0, _player.GetSpeed());
		}

        _player.Velocity = velocity;
		_player.MoveAndSlide();
    }

    public override void HandleInput(InputEvent @event)
    {
		if (@event.IsActionPressed("jump"))
			stateMachine.TransitionTo("JumpingMovementState");
    }

}
