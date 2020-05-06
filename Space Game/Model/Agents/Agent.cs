using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space_Game
{
    public class Agent
    {
        public int HitPoints, CurrentHitpoints;
        public int ShootingSkillLevel;
        public float X, Y, distance;
        public int destinationX, destinationY;
        public string Weapon;
        public int direction;
        private bool finished;
        public int actionPoints;
        public int movementPoints;

        #region Resources
        public BitmapImage SoldierImage = new BitmapImage(new Uri("Resources/Images/Soldier.png", UriKind.Relative));
        #endregion

        public Agent()
        {
            HitPoints = 25;
            actionPoints = 10;
            movementPoints = 10;
            CurrentHitpoints = HitPoints;
            ShootingSkillLevel = 50;
            Weapon = "Laser Pistol";
        }

        public Agent(int x, int y) : this()
        {
            X = x;
            Y = y;
            destinationX = (int) X;
            destinationY = (int) Y;
        }

        internal void Move(int x, int y)
        {
			float Xdiff, Ydiff;
			Xdiff = x - X;
			Ydiff = y - Y;
            distance = (float) Math.Sqrt((Xdiff * Xdiff) + (Ydiff * Ydiff));
            destinationX = (int) x;
            destinationY = (int) y;
        }

        public bool UseWeapon()
        {
            int [] Result = DiceController.Roll(1, 100);
            return Result[0] < ShootingSkillLevel;
        }

        public void Render(DrawingContext dc)
        {
            float TX = X * 50 + 25;
            float TY = Y * 50 + 25;
            dc.PushTransform(new TranslateTransform(TX, TY));
            dc.PushTransform(new RotateTransform(direction));
            dc.DrawImage(SoldierImage, new Rect(-25, -25, 50, 50));
            dc.Pop();
            dc.Pop();
        }

        public bool TestLocation(int mouseX, int mouseY)
        {
            return mouseX == X && mouseY == Y;
        }

        public bool Finished()
        {
            return finished;
        }

        internal void HeadToDestination()
        {
            if (X < destinationX) X += 0.1f;
            if (X > destinationX) X -= 0.1f;
            if (Y < destinationY) Y += 0.1f;
            if (Y > destinationY) Y -= 0.1f;
        }
    }
}
