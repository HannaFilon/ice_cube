using System;
using System.Collections.Generic;
using System.Linq;

namespace TheGame
{
    public class PilotGame
    {
        private readonly Random rand = new Random();

        public string State { get; set; }
        public bool IsComplete => State.All(q => q == '1');

        public void Randomize()
        {
            var stringChars = new char[16];
            for (int i = 0; i < 16; i++)
            {
                stringChars[i] = rand.NextDouble() >= 0.5 ? '1' : '0';
            }

            State = new string(stringChars);
        }

        public void Toggle(int index)
        {
            var stringChars = State.ToCharArray();
            stringChars[index] = Not(State[index]);
            foreach (var i in Diagonals[index])
            {
                stringChars[i] = Not(State[i]);
            }
            State = new string(stringChars);
        }

        private static char Not(char ch)
        {
            return ch == '1' ? '0' : '1';
        }

        private static readonly IDictionary<int, IEnumerable<int>> Diagonals =
            new Dictionary<int, IEnumerable<int>>
            {
                [0] = new[] {1, 2, 3, 4, 8, 12},
                [1] = new[] {0, 2, 3, 5, 9, 13},
                [2] = new[] {0, 1, 3, 6, 10, 14},
                [3] = new[] {0, 1, 2, 7, 11, 15},
                [4] = new[] {0, 8, 12, 5, 6, 7},
                [5] = new[] {1, 4, 6, 7, 9, 13},
                [6] = new[] {2, 4, 5, 7, 10, 14},
                [7] = new[] {3, 4, 5, 6, 11, 15},
                [8] = new[] {0, 4, 9, 10, 11, 12},
                [9] = new[] {1, 5, 8, 10, 11, 13},
                [10] = new[] {2, 6, 8, 9, 11, 14},
                [11] = new[] {3, 7, 8, 9, 10, 15},
                [12] = new[] {0, 4, 8, 13, 14, 15},
                [13] = new[] {1, 5, 9, 12, 14, 15},
                [14] = new[] {2, 6, 10, 12, 13, 15},
                [15] = new[] {3, 7, 11, 12, 13, 14},
            };
    }
}