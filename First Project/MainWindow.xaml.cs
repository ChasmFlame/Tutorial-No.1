using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace First_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Main_VM Mainvm =new Main_VM();

        public MainWindow()
        {
			DataContext = Mainvm;
            InitializeComponent();
        }

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			Mainvm.Number++;
			InvalidateVisual();
		}
    }
}
