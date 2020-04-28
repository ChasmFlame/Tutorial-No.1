using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_Game
{
	/// <summary>
	/// Interaction logic for WorldView.xaml
	/// </summary>
	public partial class WorldView : UserControl
	{
		Timer clock;

		public WorldView()
		{
			InitializeComponent();

			clock = new Timer(refresh, this, 1000, 100);
			void refresh(object o)
			{
				Dispatcher.Invoke(() => { ((UserControl)o).InvalidateVisual(); });
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			((World)DataContext)?.Render(dc);
			
			base.OnRender(dc);
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			InvalidateVisual();
		}
	}
}
