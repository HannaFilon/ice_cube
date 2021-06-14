using Godot;

namespace TheGame
{
	public class MainMenu : ExitableControl
	{
		public override void _Process(float delta)
		{
			base._Process(delta);
			if (Input.IsActionJustPressed("ui_accept"))
			{
				GetTree().ChangeScene("res://Scenes/Level.tscn");
			}
		}
	}
}
