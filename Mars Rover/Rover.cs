using System;
using System.Linq;
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

        public char[] DirArray { get; private set; }

        public char[] CmdArray { get; private set; }

        public Rover(int x, int y)
        {
            XHigherBounds = x > XLowerBounds ? x : throw new ArgumentOutOfRangeException(nameof(x));
            YHigherBounds = y > YLowerBounds ? x : throw new ArgumentOutOfRangeException(nameof(y));

            DirArray = new char[] { 'N', 'E', 'S', 'W' };
            CmdArray = new char[] { 'M', 'L', 'R' };
        }

        public void InitialPosition(int x, int y, char dir)
        {
            if (x < XLowerBounds || x > XHigherBounds) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < YLowerBounds || y > YHigherBounds) {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            Direction = DirArray.Contains(char.ToUpperInvariant(dir)) ? char.ToUpperInvariant(dir) 
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

            if (commandList.Any(c => !CmdArray.Contains(c))) {
                throw new ArgumentException("Your command list has an invalid letter.");
            }

            foreach (var cmd in commandList)
            {
                if (cmd == CmdArray[0]) {
                    MoveForward();
                    continue;
                }

                if (cmd == CmdArray[1]) {
                    RotateLeft();
                    continue;
                }

                if (cmd == CmdArray[2]) {
                    RotateRight();
                    continue;
                }
            }

            return (xPos, yPos, Direction);
        }

        private void RotateLeft()
        {
            switch (Direction)
            {
                case 'N':
                    Direction = DirArray[3];
                    break;

                case 'E':
                    Direction = DirArray[0];
                    break;

                case 'S':
                    Direction = DirArray[1];
                    break;

                case 'W':
                    Direction = DirArray[2];
                    break;
            }
        }

        private void RotateRight()
        {
            switch (Direction)
            {
                case 'N':
                    Direction = DirArray[1];
                    break;

                case 'E':
                    Direction = DirArray[2];
                    break;

                case 'S':
                    Direction = DirArray[3];
                    break;

                case 'W':
                    Direction = DirArray[0];
                    break;
            }
        }

        private void MoveForward()
        {
            if (Direction == DirArray[0] && yPos < YHigherBounds)
            {
                yPos++;
                return;
            }

            if (Direction == DirArray[1] && xPos < XHigherBounds)
            {
                xPos++;
                return;
            }

            if (Direction == DirArray[2] && yPos > YLowerBounds)
            {
                yPos--;
                return;
            }

            if (Direction == DirArray[3] && xPos > XLowerBounds)
            {
                xPos--;
                return;
            }
        }


    }
}
