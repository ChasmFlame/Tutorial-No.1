using System;
using System.Collections.Generic;
using System.Text;

namespace Space_Game.Model
{
    class Utilities
    {
        const float sigma = 0.0001f;

        public static bool approxequals (float lhs, float rhs)
        {
            return Math.Abs(lhs - rhs) < sigma;
        }
    }
}
