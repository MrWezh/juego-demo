using Game.Characters.Enemies;
using Godot;


public partial class DuckJumpingMovementState : State
{
    private Duck _duck;

    public override void Ready()
    {
       _duck = GetParent().GetParent<Duck>();
    }
    public override void Enter()
    {
        _duck.SetAnimation("jump");

    }

    public override void Update(double delta)
    {
        if (!_duck.IsOnFloor() && _duck.Velocity.Y >= 0)
        {
                stateMachine.TransitionTo("DuckFallingMovementState");
        }
    }

    public override void UpdatePhysics(double delta)
    {
        
        Godot.Vector2 velocity = _duck.Velocity;

        if (!_duck.IsOnFloor())
        {
            velocity += _duck.GetGravity() * (float)delta;

        }

        else
        {
            velocity.Y = _duck.GetJumpVelocity();
        }

        _duck.Velocity = velocity;
        _duck.MoveAndSlide();

    }

    public override void HandleInput(InputEvent @event)
    {
    }
}
