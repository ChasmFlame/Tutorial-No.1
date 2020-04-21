using System;
using System.Collections.Generic;
using System.Text;

namespace First_Project
{
    class Main_VM
    {
        private string DisplayText;
        private int number;


        public int Number
        {
            get
			{
				return number;
			}
            set => number = value;
        }

        public int IncrementNumber ()
        {
            return ++Number;   
        }
    }
}
