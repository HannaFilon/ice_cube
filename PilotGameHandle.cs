using Godot;

namespace TheGame
{
    public class PilotGameHandle : TextureButton
    {
        private bool _state;

        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                UpdateImage();
            }
        }

        private void UpdateImage()
        {
            SetRotation(State ? Mathf.Pi/2 : 0f);
            Modulate = State ? Color.Color8(200, 255, 200) : Color.Color8(255, 200, 200);
        }
    }
}