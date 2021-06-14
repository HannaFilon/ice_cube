using Godot;

namespace TheGame
{
    public class Fridge : Area2D
    {
        [Export] public Texture OpenFridgeTexture { get; set; }

        private bool _isLocked;

        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                _isLocked = value;
                GetNode<Label>("Label").Text = value ? "locked" : "unlocked";
            }
        }

        public override void _Ready()
        {
            IsLocked = true;
        }

        public void OnUnlock()
        {
            GD.Print("Fridge unlocked");
            IsLocked = false;

            var pilotGame = GetPilotGameControl();
            pilotGame.Hide();

            var fridgeSprite = GetNode<Sprite>("FridgeBody/Sprite");
            fridgeSprite.Texture = OpenFridgeTexture;

            var fridgeLockSprite = GetNode<Sprite>("FridgeBody/Lock");
            fridgeLockSprite.Hide();
        }

        public void OnBodyEnteredActivationArea(object body)
        {
            if (IsLocked && body is Player player)
            {
                var pilotGame = GetPilotGameControl();
                pilotGame?.Show();
                pilotGame?.CallDeferred(nameof(PilotGameControl.UpdateHandles));
            }
        }

        public void OnBodyEnteredVictoryActivationArea(object body)
        {
            if (!IsLocked && body is Player player)
            {
                GetTree().ChangeScene("res://Scenes/EndScreen.tscn");
            }
        }

        private PilotGameControl GetPilotGameControl()
        {
            var pilotGame = GetTree().Root.GetNodeOrNull<PilotGameControl>("Level/PilotGameControl");
            return pilotGame;
        }

        public void OnBodyExitedActivationArea(object body)
        {
            if (IsLocked && body is Player player)
            {
                var pilotGame = GetPilotGameControl();
                pilotGame?.Hide();
            }
        }
    }
}
