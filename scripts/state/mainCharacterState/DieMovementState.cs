// ...existing code...
using System.Threading.Tasks;
using Godot;

    public partial class DieMovementState : State
    {
        private MainCharacter _player;
        private bool _deathTimerStarted = false;

        public override async void Ready()
        {
            _player = (MainCharacter)GetTree().GetFirstNodeInGroup("MainCharacterGroup");
            if (_player == null)
                return;
            if (!_player.IsNodeReady())
                await ToSignal(_player, "ready");
        }

        public override void Enter()
        {
           _player?.SetAnimation("die");

           if (!_deathTimerStarted)
           {
               _deathTimerStarted = true;
               _ = StartDeathTimer();
           }
        }

        public override void Update(double delta)
        {
            // No se lanza el temporizador aquí cada frame
        }

        private async Task StartDeathTimer()
        {
            // Espera en el hilo principal de Godot
            await ToSignal(GetTree().CreateTimer(0.9f), "timeout");
            GetTree().CallDeferred("reload_current_scene");
        }

        public override void UpdatePhysics(double delta)
        {
            if (_player == null) return;

            Vector2 velocity = _player.Velocity;
			velocity.X=0;
			velocity.Y=0; 
            velocity += _player.GetGravity() * (float)delta;
            _player.Velocity = velocity;
            _player.MoveAndSlide();
        }
    }