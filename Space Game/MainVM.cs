using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Game
{
	class MainVM : BaseClass
	{
		#region declarations
		#endregion

		#region fields
		int [] DiceResult;
		string shootResult;
		#endregion

		#region properties
		public string Result
        {
            get {
				string Output = "You Rolled ";
				for (int iterator = 0; iterator < DiceResult.Length; iterator++)
				{
					Output += DiceResult[iterator];
					if (iterator == DiceResult.Length - 1)
						Output += ".";
					else Output += ", ";
				}
				return Output;
			}
        }
		public string ShootResult
		{
			get
			{
				string Output = string.Format ("Ben shoots and {0}", shootResult);
				return Output;
			}
		}
		#endregion

		#region methods
		public MainVM ()
        {
			DiceResult = new int[3];
			DiceResult[0] = 0;
			DiceResult[1] = 0;
			DiceResult[2] = 0;
		}

		internal void RollDice(int v1, int v2)
		{
			DiceResult = DiceController.Roll(v1, v2);
			NotifyPropertyChanged(nameof (Result));
		}

		internal void ShootWeapon()
		{
			Agent Ben = new Agent();
			bool Result = Ben.UseWeapon();
			if (Result) shootResult = "hits";
			else shootResult = "misses";
			NotifyPropertyChanged(nameof(ShootResult));
		}
		#endregion
	}
}
