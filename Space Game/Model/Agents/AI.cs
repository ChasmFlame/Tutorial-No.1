using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace Space_Game
{
    class AI : Agent
    {

        public AI(int X, int Y ) : base (X , Y)
        {
            SoldierImage = new BitmapImage(new Uri("Resources/Images/Enemy.png", UriKind.Relative));
        }
    }
}
