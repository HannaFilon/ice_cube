using System;
using System.Collections.Generic;
using Godot;

namespace TheGame
{
    public class PilotGameControl : TextureRect
    {
        private Random _rand = new Random();
        private readonly List<bool> _configuration = new List<bool>(new bool[16]);
        private PilotGame _game = new PilotGame();

        [Signal]
        public delegate void GameComplete();

        public override void _Ready()
        {
            _game.Randomize();
            UpdateHandles();
        }

        public void UpdateHandles()
        {
            for (int i = 0; i < _game.State.Length; i++)
            {
                var handle = (PilotGameHandle)GetNode($"Grid/Handle{i + 1}");
                handle.State = _game.State[i] == '1';
            }
        }

        public void OnHandlePressed(int n)
        {
            _game.Toggle(n);
            for (int i = 0; i < _game.State.Length; i++)
            {
                GD.PrintRaw($"{_game.State[i]}");
            }
            if (_game.IsComplete)
                EmitSignal(nameof(GameComplete));
            UpdateHandles();
        }
    }
}
