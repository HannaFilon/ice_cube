using Godot;

namespace TheGame
{
    public class BackgroundMusic : AudioStreamPlayer
    {
        [Export] public int StartVolume { get; set; } = -40;
        [Export] public int EndVolume { get; set; } = -5;
        [Export] public int TransitionLength { get; set; } = 3;

        public override void _Ready()
        {
            var fadeIn = GetNode<Tween>("FadeIn");
            fadeIn.InterpolateProperty(this, nameof(VolumeDb), StartVolume, EndVolume, TransitionLength, Tween.TransitionType.Linear, Tween.EaseType.In,
                0);
            fadeIn.Start();
            Play();
        }
    }
}
