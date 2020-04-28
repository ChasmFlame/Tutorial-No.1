using System;
using System.Collections.Generic;
using System.Text;

namespace Space_Game
{
    public class Agent
    {
        public int HitPoints, CurrentHitpoints;
        public int ShootingSkillLevel;
        public float X, Y;
        public string Weapon;
        public int direction;
        private int v1;
        private int v2;

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
    }
}
