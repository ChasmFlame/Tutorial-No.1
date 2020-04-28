using System;
using System.Collections.Generic;
using System.Text;

namespace Space_Game
{
    public class Player : Agent
    {
        public string Name;

        public Player(string name, int X, int Y) : base(X, Y)
        {
            Name = name;
        }
    }
}
