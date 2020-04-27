using System;
using System.Collections.Generic;
using System.Text;

namespace Space_Game
{
    public class Agent
    {
        int HitPoints, CurrentHitpoints;
        int ShootingSkillLevel;
        float X, Y;
        public Agent()
        {
            HitPoints = 25;
            CurrentHitpoints = HitPoints;
            ShootingSkillLevel = 50;
        }

        public bool UseWeapon()
        {
            int [] Result = DiceController.Roll(1, 100);
            return Result[0] < ShootingSkillLevel;
        }
    }
}
