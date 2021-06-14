using Godot;

namespace TheGame
{
    public class ExitableControl : Control
    {
        public override void _Process(float delta)
        {
            base._Process(delta);

            if (Input.IsActionJustPressed("ui_cancel"))
            {
                GetTree().Quit();
            }
        }
    }
}