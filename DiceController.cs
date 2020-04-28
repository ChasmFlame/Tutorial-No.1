using System;
using System.Collections.Generic;
using System.Text;

namespace Space_Game
{
    public static class DiceController
    {
        static Random Rand = new Random();

        public static int[] Roll(int NumberofDice, int Faces)
        {
            int[] Result = new int[] { 0, 0, 0 };
            for (int iterator = 0; iterator < NumberofDice; iterator++)
            {
                Result[iterator] = Rand.Next(Faces) + 1;
                if (Result[iterator] > Faces) Result[iterator] = Faces;
                }
            return Result;
        }
    }
}
