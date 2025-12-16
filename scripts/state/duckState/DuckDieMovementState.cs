using Game.Characters.Enemies;
using Godot;
using System;
using System.Threading.Tasks;

public partial class DuckDieMovementState : State
{

	private Duck _duck;
    
    private AnimatedSprite2D sprite;
    public override void Ready()
    {
        _duck = GetParent().GetParent<Duck>();
        sprite = _duck.GetSprite();
    }

	    public override void Enter()
    {
        _duck.SetAnimation("die");

		_duck.Velocity = Vector2.Zero;
		_duck.SetPhysicsProcess(false);

		_ = RemoveAfterAnimation();

    }

	public async Task RemoveAfterAnimation()
    {
        if (_duck == null) return;

		if (sprite != null)
        {
            await ToSignal(sprite, "animation_Finished");
        } else
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");

		_duck.QueueFree();
    }
}
