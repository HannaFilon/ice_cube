using Godot;

namespace TheGame
{
    public class Snowflake : Area2D
    {
        [Export] public PackedScene Explosion { get; set; }
        [Export] public float HealingAmount = 50f;

        private bool _isEnabled = true;

        public void OnBodyEntered(object body)
        {
            if (_isEnabled && body is Player player)
            {
                player.Health += HealingAmount;
                player.SpawnPoint = Position;

                var explosionInstance = (Particles2D) Explosion?.Instance();
                if (explosionInstance != null)
                {
                    GetTree().Root.AddChild(explosionInstance);
                    explosionInstance.Position = Position;
                    explosionInstance.Emitting = true;
                }

                Disappear();
            }
        }

        private void Disappear()
        {
            Hide();
            var respawnTimer = GetNode<Timer>("RespawnTimer");
            respawnTimer.Start();
            _isEnabled = false;
        }

        public void Reappear()
        {
            Show();
            _isEnabled = true;
        }
    }
}