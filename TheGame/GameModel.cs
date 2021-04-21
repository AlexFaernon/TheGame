using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace TheGame
{
    public class GameModel
    {
        //TODO entropy
        public Level CurrentLevel;
        public UnlimitedRulebook UnlimitedRulebook;

        public void MovePlayer(MoveDirection direction)
        {
            CurrentLevel.Player.Move(direction);
            if (CurrentLevel.ConnectedLevels.TryGetValue(CurrentLevel.Player.Position, out var connectedLevel))
                CurrentLevel = connectedLevel;
        }

        public void UseBook(string words)
        {
            if (UnlimitedRulebook)
        }
    }
    public class Level
    {
        public Map Map;
        public Player Player;
        public Dictionary<Point, Level> ConnectedLevels;
    }

    public class Map
    {
        private readonly string[] walls;
        
    }
    
    public class UnlimitedRulebook
    {
        public readonly string[] OrdinaryWords;
        public readonly MagicWord[] MagicWords;

        public UnlimitedRulebook(string[] ordinaryWords, MagicWord[] magicWords)
        {
            if (ordinaryWords.Length != magicWords.Length + 1)
                throw new ArgumentException();
            OrdinaryWords = ordinaryWords;
            MagicWords = magicWords;
        }

        public void UseWords(string words)
        {
            if (MagicWords.TryGetValue())
        }
        
        public override string ToString()
        {
            var result = new StringBuilder();
            var isActive = false;
            for (var i = 0; i < MagicWords.Length + OrdinaryWords.Length; i++)
            {
                if (isActive)
                { 
                    result.Append(MagicWords[i / 2].Value);
                }
                else
                {
                    result.Append(OrdinaryWords[i / 2]);
                }
                isActive = !isActive;
            }
            
            return result.ToString();
        }
    }

    public class MagicWord
    {
        private readonly Action magic;
        private readonly string inactiveWord;
        private readonly string activeWord;
        private bool isActive;
        private readonly int entropyValue;

        public string Value => isActive ? activeWord : inactiveWord;

        public MagicWord(Action action, string firstState, string secondState, int entropy)
        {
            magic = action;
            inactiveWord = firstState;
            activeWord = secondState;
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