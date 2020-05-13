using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space_Game
{
	public class World : BaseClass
	{
		// constants
		const int TileSize = 50;
		const int ViewSize = 450;

		const int floor = 0;

		#region Resources
		BitmapImage FloorImage = new BitmapImage(new Uri("Resources/Images/Floor.png", UriKind.Relative));
		BitmapImage CoverImage = new BitmapImage(new Uri("Resources/Images/Cover.png", UriKind.Relative));
		BitmapImage DirectionIndicator = new BitmapImage(new Uri("Resources/Images/DI.png", UriKind.Relative));
		#endregion

		#region Fields
		int[,] Tile;
		int tileX, tileY;
		ObservableCollection<string> textLines;
		private string message;
		private double direction;
		List<Agent> agents;
		int MouseX;
		int MouseY;
		Brush SelectionBrush;
		Brush SelectedAgentBrush;


		Agent selectedAgent;
		TurnManager TurnManager;
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

		public Agent SelectedAgent { get => selectedAgent; set => selectedAgent = value; }
		#endregion

		public World()
		{
			SelectionBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));
			SelectedAgentBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
			SelectedAgent = null;
			agents = new List<Agent>();
			TextLines = new ObservableCollection<string>();
			Tile = new int[10, 10]
			   {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
				{1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
				{1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
				{1, 0, 0, 0, 2, 0, 0, 0, 0, 1 }, 
				{1, 0, 0, 0, 1, 0, 2, 0, 0, 1 }, 
				{1, 0, 0, 0, 1, 0, 0, 0, 0, 1 }, 
				{1, 0, 0, 0, 2, 2, 1, 0, 0, 1 }, 
				{1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
				{1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
				};

			Message = "Ready!";
			agents.Add(new Player("Ben", 1, 1));
			agents.Add(new AI(8, 8));
			TurnManager = new TurnManager();
			tileX = -1;
			tileY = -1;
		}

		internal void MoveAgent()
		{
			if (tileX == -1 || tileY == -1) return;
			SelectedAgent?.Move(tileX, tileY);
			tileX = tileY = -1;
		}

		public bool checklineofsight (float startx, float starty, float endx, float endy)
		{
			float xlength, ylength, xdelta, ydelta, length;
			bool CanSee = true;
			xlength = endx - startx;
			ylength = endy - starty;
			startx += 0.5f;
			starty += 0.5f;
			if (xlength > ylength)
			{
				length = xlength;
				xdelta = 1;
				ydelta = ylength / xlength;
				ydelta = Math.Sign(ylength) * ydelta;
			}
			else
			{
				length = ylength;
				ydelta = 1;
				xdelta = xlength / ylength;
				xdelta = Math.Sign(xlength) * xdelta;
			}
			for (float iterator = 1; iterator < length; iterator++)
			{
				if (Tile [(int) (startx + xdelta*iterator) , (int) (starty + ydelta * iterator)] > 0) CanSee = false;
			}

			return CanSee;
		}

		private void AddTextLine(string text)
		{
			TextLines.Add(text);
			NotifyPropertyChanged(nameof(TextLines));
		}

		public void Render(DrawingContext dc)
		{
			RunLogic();
			dc.DrawRectangle(Brushes.Black, null, new Rect(0, 0, ViewSize, ViewSize));
			for(int x = 0; x < 9; x++)
				for(int y = 0; y < 9; y++)
				{
					int xpos = TileSize * x;
					int ypos = TileSize * y;
					switch(Tile[x, y])
					{
						case 0:
						dc.DrawImage(FloorImage, new Rect(xpos, ypos, TileSize, TileSize)) ;
							break;
						case 2:
							dc.DrawImage(CoverImage, new Rect(xpos, ypos, TileSize, TileSize));
							break;
					}
				}
			if (SelectedAgent != null)
				dc.DrawRectangle(SelectedAgentBrush, null, new Rect(SelectedAgent.X*TileSize, SelectedAgent.Y*TileSize, TileSize, TileSize));

			dc.DrawRectangle(SelectionBrush, null, new Rect(tileX * TileSize, tileY * TileSize, TileSize, TileSize));

			foreach (Agent agent in agents)
			{
				agent.Render(dc);
			}

			dc.PushTransform(new TranslateTransform(ViewSize/2, ViewSize/2)); 
			dc.PushTransform(new RotateTransform(direction));
			dc.DrawImage(DirectionIndicator, new Rect(-TileSize/2, -TileSize/2, TileSize, TileSize));
			dc.Pop();
			dc.Pop();
		}

		private void RunLogic()
		{
			Move();
			TurnManager.CheckStateChanges(this);
			Message = TurnManager.GetStateMessage();
		}

		public void Move()
		{
			foreach (Agent agent in agents)
			{
				agent.HeadToDestination();
			}
		}

		public void MouseClick(double x, double y)
		{
			MouseX = (int)x / 50;
			MouseY = (int)y / 50;
			if (Tile [MouseX, MouseY] == 0 && selectedAgent != null && checklineofsight (selectedAgent.X , selectedAgent.Y, MouseX , MouseY ) )
				{ 
				tileX = MouseX;
				tileY = MouseY;
			}
			foreach(Agent agent in agents)
			{
				if(agent.TestLocation(MouseX, MouseY))
				{
					selectedAgent = agent;
				}
			}
		}

		public bool MovementComplete()
		{
			bool AllMoved = true;
			foreach(Agent agent in agents)
			{
				if(!agent.Finished()) AllMoved = false;
			}
			return AllMoved;
		}

		public void ShootWeapon()
		{
			Player player = (Player)agents[0];
			string shootResult;
			if(agents[0].UseWeapon())
				shootResult = "hits";
			else
				shootResult = "misses";
			AddTextLine(String.Format("{0} shoots his {2} and {1}!", player.Name, shootResult, player.Weapon));
		}
	}
}
