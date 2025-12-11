using Game.Characters.Enemies;
using Godot;
using System;

public partial class DuckIdleMovementState : State
{
    private Duck _duck;

    public override void Ready()
    {
        _duck = GetParent().GetParent<Duck>();
    }
    public override void Enter()
    {
		GD.Print("Hola soy pato");
        _duck.SetAnimation("idle");
    }

    public override void Update(double delta)
    {
    }

	public override void UpdatePhysics(double delta)
	{
	}
    public override void HandleInput(InputEvent @event)
    {
    }
}
