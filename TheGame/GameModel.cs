using System.Collections.Generic;
using System.Drawing;

namespace TheGame
{
    public class Level
    {
        public Map Map;
        public Player Player;
        public Dictionary<Door, Level> ConnectedLevels;
    }

    public class Map
    {
        private readonly string[] walls;
        
    }
    
    public class UnlimitedRuleBook
    {
        
    }
    
    public class Player
    {
        public Point Position { get; private set; }

        public void Move(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    Position += new Size(0,1);
                    break;
                case MoveDirection.Down:
                    Position += new Size(0,-1);
                    break;
                case MoveDirection.Right:
                    Position += new Size(1,0);
                    break;
                case MoveDirection.Left:
                    Position += new Size(-1,0);
                    break;
            }
        }
    }

    public class Door
    {
        public bool IsOpen { get; private set; }

        public void ChangeState()
        {
            IsOpen = !IsOpen;
        }
    }

    public class Switcher
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
                door.ChangeState();
            }
        }
    }
}