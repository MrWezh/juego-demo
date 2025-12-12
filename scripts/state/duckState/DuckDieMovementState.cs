using Game.Characters.Enemies;
using Godot;
using System;
using System.Threading.Tasks;

public partial class DuckDieMovementState : State
{

	private Duck _duck;
	private AnimatedSprite2D _sprite;

    public override void Ready()
    {
        _duck = GetParent().GetParent<Duck>();
    }

	    public override void Enter()
    {
        _duck.SetAnimation("die");

		_duck.Velocity = Vector2.Zero;
		_duck.SetPhysicsProcess(false);

		_duck.CollisionLayer = 0;
		_duck.CollisionMask = 0;

		_ = RemoveAfterAnimation();

    }

	public async Task RemoveAfterAnimation()
    {
        if (_duck == null) return;

		_sprite = GetParent().GetNode<AnimatedSprite2D>("Sprite");

		if (_sprite != null)
        {
            await ToSignal(_sprite, "animation_Finished");
        } else
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");

		_duck.QueueFree();
    }
}
