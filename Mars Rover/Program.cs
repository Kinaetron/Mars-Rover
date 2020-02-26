using System;
using System.Linq;

namespace Mars_Rover
{
    class Program
    {
        private static Rover rover;
        private static bool exitApp = false;
        private static string movements = null;

        static void Main(string[] args)
        {
            while (!exitApp)
            {
                while (rover == null) {
                    rover = InitRover();
                }

                if (!RoverPosition()) {
                    continue;
                }

                while (movements == null) {
                    movements = RoverMovement();
                }

                var results = rover.Move(movements);

                Console.WriteLine("The results of the rover are {0} {1} {2}", results.Item1, results.Item2, results.Item3);

                Console.WriteLine("Do you want to continue (Y / N)");
                var quit = Console.ReadLine();

                if(quit.ToUpperInvariant() == "N") {
                    exitApp = true;
                }

                movements = null;
            }
        }

        private static Rover InitRover()
        {
            Console.WriteLine("Please enter the upper right coordinates in the format X Y");
            var initCoordinates = Console.ReadLine().Split(" ");

            if (initCoordinates.Length != 2) {
                return null;
            }

            if (!int.TryParse(initCoordinates[0], out int x) ||
                !int.TryParse(initCoordinates[1], out int y)) {

                return null;
            }

            return new Rover(x, y);
        }

        private static bool RoverPosition()
        {
            Console.WriteLine("Please enter your rovers intial position");
            var intialPos = Console.ReadLine().Split(" ");

            if (intialPos.Length != 3) {
                return false;
            }

            if (!int.TryParse(intialPos[0], out int rovX) ||
               !int.TryParse(intialPos[1], out int rovY) ||
               !char.TryParse(intialPos[2], out char dir)) {
                return false;
            }

            if (!rover.DirArray.Contains(char.ToUpperInvariant(dir))) {
                return false;
            }

            rover.InitialPosition(rovX, rovY, dir);
            return true;
        }

        private static string RoverMovement()
        {
            Console.WriteLine("Please enter your rovers movements");
            var movements = Console.ReadLine();

            if (movements.ToUpperInvariant().IndexOfAny(rover.CmdArray) != 0) {
                return null;
            }

            return movements;
        }
    }
}
