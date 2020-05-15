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
							dc.DrawImage(FloorImage, new Rect(xpos, ypos, TileSize, TileSize));
							break;
						case 2:
							dc.DrawImage(CoverImage, new Rect(xpos, ypos, TileSize, TileSize));
							break;
					}
				}
			if(SelectedAgent != null)
				DrawHighlight(dc, SelectedAgentBrush, new Point(SelectedAgent.X, SelectedAgent.Y), .8f);

			DrawHighlight(dc, SelectionBrush, new Point(tileX, tileY), .4f);

			foreach(Agent agent in agents)
			{
				agent.Render(dc);
			}

			dc.PushTransform(new TranslateTransform(ViewSize / 2, ViewSize / 2));
			dc.PushTransform(new RotateTransform(direction));
			dc.DrawImage(DirectionIndicator, new Rect(-TileSize / 2, -TileSize / 2, TileSize, TileSize));
			dc.Pop();
			dc.Pop();
		}

		#region Input Control
		public void MouseClick(double x, double y)
		{
			MouseX = (int)x / 50;
			MouseY = (int)y / 50;
			if(Tile[MouseX, MouseY] == 0 
				&& selectedAgent != null 
				&& selectedAgent.CheckLineOfSight(Tile, MouseX, MouseY) == Model.Visibility.CLEAR)
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

		public void MoveOption()
		{
			if(tileX == -1 || tileY == -1) return;
			SelectedAgent?.Move(tileX, tileY);
			tileX = tileY = -1;
		}

		public void UseWeaponOption()
		{
		}
		#endregion

		#region Game Logic
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

		public bool MovementComplete()
		{
			bool AllMoved = true;
			foreach(Agent agent in agents)
			{
				if(!agent.HasMovementCompleted()) AllMoved = false;
			}
			return AllMoved;
		}
		#endregion

		#region Utility 
		private void AddTextLine(string text)
		{
			TextLines.Add(text);
			NotifyPropertyChanged(nameof(TextLines));
		}

		private void DrawHighlight(DrawingContext dc, Brush brush, Point point, float size)
		{
			size = size * (float)TileSize/2;
			dc.DrawEllipse(brush, null, new Point((point.X + 0.5f) * TileSize, (point.Y + 0.5f) * TileSize), size, size);
		}
		#endregion
	}
}
