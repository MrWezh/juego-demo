using Godot;
using System;
using Game.Characters.Enemies;

public partial class DuckFallingMovementState : State
{
    private Duck _duck;

    public override void Ready()
    {
		_duck = GetParent().GetParent<Duck>();
    }

    public override void Enter()
    {
        _duck.SetAnimation("fall");
    }

    public override void Update(double delta)
    {
		if (_duck.IsOnFloor())
		{
			if (_duck.Velocity.X == 0)
				stateMachine.TransitionTo("DuckIdleMovementState");
        }

    
    }

    public override void UpdatePhysics(double delta)
    {

        Vector2 velocity = _duck.Velocity;
        if (!_duck.IsOnFloor())
        {
            velocity += _duck.GetGravity() * (float)delta;

        }
        _duck.Velocity = velocity;
    }

    public override void HandleInput(InputEvent @event)
    { }
}
