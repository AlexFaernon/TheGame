using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace TheGame
{
    public class GameModel
    {
        public int currentEntropy = 0;
        public Level CurrentLevel;
        public UnlimitedRulebook UnlimitedRulebook;
        public Player Player => CurrentLevel.Player;

        public GameModel(Level level)
        {
            CurrentLevel = level;
        }

        public void MovePlayer(MoveDirection direction)
        {
            CurrentLevel.Player.Move(direction, CurrentLevel.Map);
            if (CurrentLevel.ConnectedLevels.TryGetValue(CurrentLevel.Player.Position, out var connectedLevel))
                CurrentLevel = connectedLevel;
        }
    }
    public class Level
    {
        public readonly Map Map;
        public readonly Player Player = new Player();
        public readonly Dictionary<Point, Level> ConnectedLevels = new Dictionary<Point, Level>();

        public Level(Map map)
        {
            Map = map;
        }
    }

    public class Map
    {
        private readonly bool[,] walls;

        public Dictionary<Point, IGameObject> GameObjects;
        public int Width => walls.GetLength(0);
        public int Height => walls.GetLength(1);

        public Map(string[] map)
        {
            walls = new bool[map.Length,map[0].Length];
            
            for (var x = 0; x < map.Length; x++)
            {
                for (var y = 0; y < map[0].Length; y++)
                {
                    if (map[x][y] != '#')
                    {
                        walls[x, y] = true;
                    }
                }
            }
        }

        public bool IsCorrect(Point point)
        {
            if (point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height)
            {
                return walls[point.X, point.Y];
            }

            return false;
        }
        
    }
    
    public class UnlimitedRulebook
    {
        public readonly MagicWords[] MagicWords;

        public UnlimitedRulebook(MagicWords[] magicWords)
        {
            MagicWords = magicWords;
        }
    }

    public class MagicWords
    {
        private readonly Action magic;
        private bool isActive;
        private readonly int entropyValue;

        public MagicWords(Action action, int entropy)
        {
            magic = action;
            entropyValue = entropy;
        }

        public int DoMagic()
        {
            isActive = !isActive;
            magic();
            return entropyValue;
        }
    }
    
    public class Player
    {
        public Point Position { get; private set; }

        public Player()
        {
            
        }

        public Player(Point start)
        {
            Position = start;
        }
            public void Move(MoveDirection direction, Map map)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    if (map.IsCorrect(Position + new Size(0,-1)))
                        Position += new Size(0,-1);
                    break;
                case MoveDirection.Down:
                    if (map.IsCorrect(Position + new Size(0,1)))
                        Position += new Size(0,1);
                    break;
                case MoveDirection.Right:
                    if (map.IsCorrect(Position + new Size(1,0)))
                        Position += new Size(1,0);
                    break;
                case MoveDirection.Left:
                    if (map.IsCorrect(Position + new Size(-1,0)))
                        Position += new Size(-1,0);
                    break;
            }
        }
    }

    public class Door : IGameObject
    {
        public bool IsOpen { get; private set; }
        
        public void Switch()
        {
            IsOpen = !IsOpen;
        }
    }

    public class Switcher : IGameObject
    {
        private readonly Door[] connectedDoors;

        public Switcher(Door[] doors)
        {
            connectedDoors = doors;
        }

        public void Switch()
        {
            foreach (var door in connectedDoors)
            {
                door.Switch();
            }
        }
    }
}