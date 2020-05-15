using System;
using System.Collections.Generic;
using System.Text;

namespace Space_Game.Model
{
    public enum Visibility
    {
        CLEAR,
        UNCLEAR,
        OBSCURED,
        HIDDEN
    }

    class Utilities
    {
        const float sigma = 0.0001f;

        public static bool ApproxEqual (float lhs, float rhs)
        {
            return Math.Abs(lhs - rhs) < sigma;
        }
    }
}
