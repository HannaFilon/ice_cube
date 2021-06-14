using Godot;

namespace AlienShooter.Classes
{
    public abstract class Oscillator : KinematicBody2D
    {
        [Export] public float OscillationFrequency { get; set; } = 1.0f;
        [Export] public float OscillationAmplitude { get; set; } = 125.0f;
        [Export] public float OscillationOffset { get; set; } = 0.0f;

        protected Vector2 Velocity = new Vector2(0, 1);
        
        protected float TimeOffset;
        
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            TimeOffset += delta;
            Velocity.y = Mathf.Cos(OscillationFrequency * TimeOffset + OscillationOffset) * OscillationAmplitude;

            MoveAndCollide(Velocity * delta);
        }
    }
}