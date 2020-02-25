using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mars_Rover
{
    public class Rover
    {
        private int xPos;
        private int yPos;


        public char Direction { get; private set; }

        public int XLowerBounds { get; private set; } = 0;
        public int YLowerBounds { get; private set; } = 0;

        public int XHigherBounds { get; private set; }
        public int YHigherBounds { get; private set; }

        public List<char> DirList { get; private set; }

        public List<char> CmdList { get; private set; }

        public Rover(int x, int y)
        {
            XHigherBounds = x > XLowerBounds ? x : throw new ArgumentOutOfRangeException(nameof(x));
            YHigherBounds = y > YLowerBounds ? x : throw new ArgumentOutOfRangeException(nameof(y));

            DirList = new List<char> { 'N', 'E', 'S', 'W' };
            CmdList = new List<char> { 'M', 'L', 'R' };
        }

        public void InitialPosition(int x, int y, char dir)
        {
            if (x < XLowerBounds || x > XHigherBounds) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < YLowerBounds || y > YHigherBounds) {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            Direction = DirList.Contains(char.ToUpperInvariant(dir)) ? char.ToUpperInvariant(dir) 
                    : throw new ArgumentException("Direction specified isn't valid.");

            xPos = x;
            yPos = y;
        }

        public (int, int, char) Move(string commands)
        {
            if (string.IsNullOrWhiteSpace(commands)) {
                throw new ArgumentNullException(nameof(commands));
            }

            var commandList = Regex.Replace(commands, @"s", "").ToUpper().Where(c => char.IsLetter(c));

            if (commandList.Any(c => !CmdList.Contains(c))) {
                throw new ArgumentException("Your command list has an invalid letter.");
            }

            foreach (var cmd in commandList)
            {
                if (cmd == CmdList[0]) {
                    MoveForward();
                }

                if (cmd == CmdList[1]) {
                    RotateLeft();
                }

                if (cmd == CmdList[2]) {
                    RotateRight();
                }
            }

            return (xPos, yPos, Direction);
        }

        private void RotateLeft()
        {
            switch (Direction)
            {
                case 'N':
                    Direction = DirList[3];
                    break;

                case 'E':
                    Direction = DirList[0];
                    break;

                case 'S':
                    Direction = DirList[1];
                    break;

                case 'W':
                    Direction = DirList[2];
                    break;
            }
        }

        private void RotateRight()
        {
            switch (Direction)
            {
                case 'N':
                    Direction = DirList[1];
                    break;

                case 'E':
                    Direction = DirList[2];
                    break;

                case 'S':
                    Direction = DirList[3];
                    break;

                case 'W':
                    Direction = DirList[0];
                    break;
            }
        }

        private void MoveForward()
        {
            if (Direction == DirList[0] && yPos < YHigherBounds)
            {
                yPos++;
                return;
            }

            if (Direction == DirList[1] && xPos < XHigherBounds)
            {
                xPos++;
                return;
            }

            if (Direction == DirList[2] && yPos > YLowerBounds)
            {
                yPos--;
                return;
            }

            if (Direction == DirList[3] && xPos > XLowerBounds)
            {
                xPos--;
                return;
            }
        }


    }
}
