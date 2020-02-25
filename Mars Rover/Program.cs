using System;

namespace Mars_Rover
{
    class Program
    {
        private static Rover rover;
        private static bool exitApp = false;

        static void Main(string[] args)
        {
            while (!exitApp)
            {
                if(rover == null)
                {
                    Console.WriteLine("Please enter the upper right coordinates in the format X Y");
                    var initCoordinates = Console.ReadLine().Split(" ");

                    if (initCoordinates.Length != 2) {
                        continue;
                    }

                    if (!int.TryParse(initCoordinates[0], out int x) ||
                        !int.TryParse(initCoordinates[1], out int y)) {

                        continue;
                    }

                    rover = new Rover(x, y);
                }

                Console.WriteLine("Please enter your rovers intial position");
                var intialPos = Console.ReadLine().Split(" ");

                if(intialPos.Length != 3) {
                    continue;
                }

                if(!int.TryParse(intialPos[0], out int rovX) || 
                   !int.TryParse(intialPos[1], out int rovY) ||
                   !char.TryParse(intialPos[2], out char dir)) {
                    continue;
                }

                if(!rover.DirList.Contains(char.ToUpperInvariant(dir))) {
                    continue;
                }

                rover.InitialPosition(rovX, rovY, dir);

                Console.WriteLine("Please enter your rovers movements");
                var movements = Console.ReadLine();


                var results = rover.Move(movements);

                Console.WriteLine("The results of the rover are {0} {1} {2}", results.Item1, results.Item2, results.Item3);

                Console.WriteLine("Do you want to continue (Y / N)");
                var quit = Console.ReadLine();

                if(quit.ToUpperInvariant() == "N") {
                    exitApp = true;
                }
            }
        }
    }
}
