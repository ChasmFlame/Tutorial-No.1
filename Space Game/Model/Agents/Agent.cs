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
        public float X, Y;
        public string Weapon;
        public int direction;
       
        #region Resources
        public BitmapImage SoldierImage = new BitmapImage(new Uri("Resources/Images/Soldier.png", UriKind.Relative));
        #endregion

        public Agent()
        {
            HitPoints = 25;
            CurrentHitpoints = HitPoints;
            ShootingSkillLevel = 50;
            Weapon = "Laser Pistol";
        }

        public Agent(int X, int Y):this()
        { 
            this.X = X;
            this.Y = Y;
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

        internal bool TestLocation(int mouseX, int mouseY)
        {
            return mouseX == X && mouseY == Y;
        }
    }
}
