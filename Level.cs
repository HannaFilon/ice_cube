using Godot;

namespace TheGame
{
	public class Level : Node2D
	{
		public override void _Ready()
		{
			var player = (Player)GetNode("Player");
			var respawnLine = (Position2D)GetNode("RespawnLine");
			player.RespawnY = respawnLine.Position.y;

			var pilotGameControl = GetNode<PilotGameControl>("PilotGameControl");
			var fridge = GetNode<Fridge>("Fridge");

			pilotGameControl.Connect(nameof(PilotGameControl.GameComplete), fridge, nameof(Fridge.OnUnlock));
		}

		public override void _Process(float delta)
		{
			var player = (Player) GetNode("Player");
			var camera = (Camera2D) GetNode("Camera");
			camera.Position = new Vector2(player.Position.x, camera.Position.y);

			if (Input.IsActionJustPressed("ui_cancel"))
			{
                GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
			}
		}

        public void OnBackgroundMusic1Finished()
        {
			GD.Print("Background music 1 finished");
            var backgroundMusic2 = GetNodeOrNull< AudioStreamPlayer > ("BackgroundMusic2");
			backgroundMusic2?.Play();
        }
	}
}
