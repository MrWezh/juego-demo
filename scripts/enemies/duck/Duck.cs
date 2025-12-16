using Godot;

namespace Game.Characters.Enemies
{
    public partial class Duck : CharacterBody2D
    {
        private AnimatedSprite2D _sprite;
        private StateMachine stateMachine;

        private float jumpVelocity = -300.0f;

     [Signal] public delegate void KilledEventHandler();

        public override void _Ready()
        {
            _sprite = GetNode<AnimatedSprite2D>("Sprite");
            stateMachine = GetNode<StateMachine>("DuckMovementState");


        }

        public override void _PhysicsProcess(double delta)
        {
            KillPlayer();
        }

        public void KillPlayer()
        {
              // Mover primero
            MoveAndSlide();

            // Revisar colisiones resultantes
            for (int i = 0; i < GetSlideCollisionCount(); ++i)
            {
                var collision = GetSlideCollision(i);
                if (collision == null) continue;

                var collider = collision.GetCollider();
                if (collider is MainCharacter player)
                {
                        player.Kill();
                    }
                }
            
        }
        
        public AnimatedSprite2D GetSprite()
        {
            return _sprite;         
        }
        public float GetJumpVelocity()
        {
            return jumpVelocity;
        }

        public void setJujmpVelocity(float new_jumpVelocity)
        {
            jumpVelocity = new_jumpVelocity;
        }

        public void SetAnimation(string new_animation)
        {
            if (_sprite == null) return;

            _sprite.Play(new_animation);
        }

        public void OnPlayerJumped()
        {
            SetAnimation("before_jump"); 
            stateMachine.TransitionTo("DuckJumpingMovementState");
        }

        public void _on_jump_detected_body_entered(Node2D body)
        {
            if (body.GetType() == typeof(MainCharacter))
                {
                    ((MainCharacter)body).Jumped += OnPlayerJumped;
                }
        }

        public void _on_jump_detected_body_exited(Node2D body)
        {
            if (body.GetType() == typeof(MainCharacter))
            {
                 ((MainCharacter)body).Jumped -= OnPlayerJumped;
            }
        }

        public void _on_kill_area_body_entered(Node2D body)
        {

            if (body.GetType() == typeof(MainCharacter))
            {
                CollisionLayer = 0;
		        CollisionMask = 0;
                stateMachine.TransitionTo("DuckDieMovementState");

            }
            EmitSignal("Killed"); 
            
        }

    }
}