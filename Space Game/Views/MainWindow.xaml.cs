using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Space_Game
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainVM vm = new MainVM();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = vm;

		
		}

		private void Button_Shoot_Click(object sender, RoutedEventArgs e)
		{
			vm.ShootWeapon();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			//vm.KeyPressed();
		}

		private void DataGrid_SourceUpdated(object sender, DataTransferEventArgs e)
		{
			DataGrid textLines = (DataGrid)sender;

			if(textLines.Items.Count > 0)
			{
				var border = VisualTreeHelper.GetChild(textLines, 0) as Decorator;
				if(border != null)
				{
					var scroll = border.Child as ScrollViewer;
					if(scroll != null) scroll.ScrollToEnd();
				}
			}
		}

		private void Button_Move_Click(object sender, RoutedEventArgs e)
		{
			vm.MoveAgent();
		}
	}
}
