using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Space_Game
{

	public class BaseClass : DependencyObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
			Application.Current?.Dispatcher.Invoke(
				() =>
				{
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
				});
		}
		protected virtual void ExternalPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e?.PropertyName != "")
				NotifyPropertyChanged(e.PropertyName);
		}
	}
}