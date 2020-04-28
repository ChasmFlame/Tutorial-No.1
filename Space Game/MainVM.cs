using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Space_Game
{
	class MainVM : BaseClass
	{
		#region fields
		
		#endregion

		#region properties
		public World World
		{
			get;set;
		}
		public ObservableCollection<string> TextLines
        {
            get {				return new  ObservableCollection<string>( World.TextLines);			}
        }
		public String Message
		{
			get
			{
				return World.Message;
			}
		}
		#endregion

		#region methods
		public MainVM ()
        {
			World = new World();
			World.PropertyChanged += UpdateProperties;
		}

		private void UpdateProperties(object sender, PropertyChangedEventArgs e)
		{
			NotifyPropertyChanged(e.PropertyName);
		}

		internal void ShootWeapon()
		{
			World.ShootWeapon();
		}
		#endregion
	}
}
