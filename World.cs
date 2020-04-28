using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Space_Game
{
	public class World : BaseClass
	{
		#region Resources
		BitmapImage FloorImage = new BitmapImage(new Uri("Resources/Images/Floor.png", UriKind.Relative));
		BitmapImage CoverImage = new BitmapImage(new Uri("Resources/Images/Cover.png", UriKind.Relative));
		BitmapImage DirectionIndicator = new BitmapImage(new Uri("Resources/Images/DI.png", UriKind.Relative));
		#endregion

		#region Fields
		int[,] Tile;
		ObservableCollection<string> textLines;
		private string message;
		private double direction;
		#endregion

		#region Properties
		public ObservableCollection<string> TextLines
		{
			get
			{
				return textLines;
			}
			set	
			{
				textLines = value;
				NotifyPropertyChanged();
			}	
		}
		public string Message
		{
			get
			{
				return message;
			}
			set
			{
				message = value;
				NotifyPropertyChanged();
			}		
		}
		#endregion

		public World()
		{
			TextLines = new ObservableCollection<string>();
			Tile = new int[10, 10]
			   {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				};

			Message = "Ready!";
		}

		internal void ShootWeapon()
		{
			string shootResult;
			Agent Ben = new Agent();

			if(Ben.UseWeapon())
				shootResult = "hits";
			else
				shootResult = "misses";
			AddTextLine(String.Format("Ben shoots his weapon and {0}!", shootResult));
		}

		private void AddTextLine(string text)
		{
			TextLines.Add(text);
			NotifyPropertyChanged(nameof(TextLines));
		}

		public void Render(DrawingContext dc)
		{
			direction += 5;
			dc.DrawRectangle(Brushes.Black, null, new Rect(0, 0, 450, 450));
			for(int x = 0; x < 9; x++)
				for(int y = 0; y < 9; y++)
				{
					int xpos = 50 * x;
					int ypos = 50 * y;
					switch(Tile[x, y])
					{
						case 1:
						dc.DrawImage(FloorImage, new Rect(xpos, ypos, 50, 50)) ;
							break;
						case 2:
							dc.DrawImage(CoverImage, new Rect(xpos, ypos, 50, 50));
							break;
					}
				}

			dc.PushTransform(new TranslateTransform(225, 225)); 
			dc.PushTransform(new RotateTransform(direction));
			dc.DrawImage(DirectionIndicator, new Rect(-25, -25, 50, 50));
			dc.Pop();
			dc.Pop();
		}
	}
}
