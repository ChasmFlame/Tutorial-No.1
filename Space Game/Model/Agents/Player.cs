using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Space_Game
{
    public class Player : Agent
    {
        public string Name;

        public Player(string name, int X, int Y) : base(X, Y)
        {
            Name = name;
            SoldierImage = new BitmapImage(new Uri("Resources/Images/Soldier.png", UriKind.Relative));
            direction = 90;
        }
    }
}
