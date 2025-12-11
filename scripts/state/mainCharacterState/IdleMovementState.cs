using Godot;
using System;

public partial class IdleMovementState : State
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
        _player.SetAnimation("default");
    }

    public override void Update(double delta)
    {

      if (!_player.IsOnFloor())
        {
            Vector2 velocity = _player.Velocity;
            velocity += _player.GetGravity() * (float)delta;
            if (_player.Velocity.Y < 0)
                stateMachine.TransitionTo("JumpingMovementState");
            else
                stateMachine.TransitionTo("FallingMovementState");
        }
        else 
        _player.setdoubleJump(true);

      float direction = Input.GetAxis("move_left", "move_right");


        if (direction != 0.0f)
        {
            stateMachine.TransitionTo("RunningMovementState");
        }
    }
    public override void HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("move_left") || @event.IsActionPressed("move_right"))
            stateMachine.TransitionTo("RunningMovementState");
        if (@event.IsActionPressed("jump"))
            stateMachine.TransitionTo("JumpingMovementState");
    }
}
