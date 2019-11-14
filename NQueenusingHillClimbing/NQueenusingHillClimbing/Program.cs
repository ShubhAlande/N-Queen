using System;

namespace NQueenusingHillClimbing
{
    class Program
    {
        static void Main(string[] args)
        {            
            string _formatter="##.##";
            Random rand = new Random();
            int _noOfIterations = rand.Next(500) + 100;
            int queen = 0;

            int[] steepestAscentResults = { 0, 0, 0, 0 };
            int[] Sideways = { 0, 0, 0, 0 };
            int[] Restart = { 0, 0, 0, 0 };
            int[] RestartWithSideWay = { 0, 0, 0, 0 };
            string UserDecision = string.Empty;

            do
            {
                while (queen <= 3)
                {
                    Console.Write("Enter the number of Queens (Should be more than 3): ");
                    queen = Convert.ToInt16(Console.ReadLine());
                }
                int choice = -1;
                Console.WriteLine("Select Algorithm to solve N-Queen problem ");
                Console.WriteLine("1. Steepest-Ascent");
                Console.WriteLine("2. Sideways Move");
                Console.WriteLine("3. Random Restart");
                Console.WriteLine("4. Random RestartT With Sideways Move");
                Console.Write("Select option= ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        for (int iteration = 0; iteration < _noOfIterations; iteration++)
                        {
                            SteepestAscent game = new SteepestAscent(queen);
                            int[] results = game.RunAlgorithm();
                            steepestAscentResults[0] += results[0];
                            steepestAscentResults[1] += results[1];
                            steepestAscentResults[2] += results[2];
                            steepestAscentResults[3] += results[3];
                        }
                        Console.WriteLine("\n**************************************************************************");
                        Console.WriteLine("Steepest-Ascent Hill Climbing Algorithm");
                        Console.WriteLine("Number Of Queens: " + queen);
                        Console.WriteLine("Number of Iterations: " + _noOfIterations);
                        Console.WriteLine("Success/Fail Analysis");
                        Console.WriteLine("Success Rate: {0}", ((steepestAscentResults[1] * 100) / (float)_noOfIterations + '%').ToString(_formatter));
                        Console.WriteLine("Failure Rate: {0}", ((steepestAscentResults[3] * 100) / (float)_noOfIterations + '%').ToString(_formatter));
                        if (steepestAscentResults[1] != 0)
                            Console.WriteLine("Average Number of Steps When It Succeeds: " + (steepestAscentResults[0] / steepestAscentResults[1]));
                        if (steepestAscentResults[3] != 0)
                            Console.WriteLine("Average Number of Steps When It Fails: " + (steepestAscentResults[2] / steepestAscentResults[3]));
                        //Console.ReadLine();
                        break;
                    case 2:
                        for (int iteration = 0; iteration < _noOfIterations; iteration++)
                        {
                            SidewaysMove game = new SidewaysMove(queen);
                            int[] results = game.RunAlgorithm();
                            Sideways[0] += results[0];
                            Sideways[1] += results[1];
                            Sideways[2] += results[2];
                            Sideways[3] += results[3];
                        }

                        Console.WriteLine("\n***********************************************************************");
                        Console.WriteLine("Hill Climbing Sideways Moves Algorithm");
                        Console.WriteLine("Number of Queens: " + queen);
                        Console.WriteLine("Number of Iterations: " + _noOfIterations);
                        Console.WriteLine("Success/Fail Analysis");
                        Console.WriteLine("Success Rate: {0}", ((Sideways[1] * 100) / (float)_noOfIterations + '%').ToString(_formatter));
                        Console.WriteLine("Failure Rate: {0}", ((Sideways[3] * 100) / (float)_noOfIterations + '%').ToString(_formatter));

                        if (Sideways[1] != 0)
                            Console.WriteLine("Average Number of Steps When It Succeeds: " + (Sideways[0] / Sideways[1]));
                        if (Sideways[3] != 0)
                            Console.WriteLine("Average Number of Steps When It Fails: " + (Sideways[2] / Sideways[3]));
                        //Console.ReadLine();
                        break;

                    case 3:
                        for (int iteration = 0; iteration < _noOfIterations; iteration++)
                        {
                            RandomRestart game = new RandomRestart(queen);
                            int[] results = game.RunAlgorithmWithoutSideways();
                            Restart[0] += results[0];
                            Restart[1] += results[1];
                            Restart[2] += results[2];
                        }

                        Console.WriteLine("\n******************************************************************************");
                        Console.WriteLine("Hill Climbing Random Restart Algorithm");
                        Console.WriteLine("Number of Queens: " + queen);
                        Console.WriteLine("Number of Iterations: " + _noOfIterations);
                        Console.WriteLine("Success/Fail Analysis");
                        Console.WriteLine("Success Rate: {0}", ((Restart[1] * 100) / (float)_noOfIterations + '%').ToString(_formatter));

                        Console.WriteLine("Average Number of Steps When It Succeeds: " + (Restart[0] / _noOfIterations));
                        Console.WriteLine("Average Number of Restarts When It Succeeds: " + (Restart[2] / _noOfIterations));
                        //Console.ReadLine();
                        break;

                    case 4:
                        for (int iteration = 0; iteration < _noOfIterations; iteration++)
                        {
                            RandomRestart game = new RandomRestart(queen);
                            int[] results = game.RunAlgorithmWithSideways();
                            RestartWithSideWay[0] += results[0];
                            RestartWithSideWay[1] += results[1];
                            RestartWithSideWay[2] += results[2];
                        }

                        Console.WriteLine("\n*****************************************************************************");
                        Console.WriteLine("Hill Climbing Random Restart Algorithm with sideways move");
                        Console.WriteLine("Number of Queens: " + queen);
                        Console.WriteLine("Number of Iterations: " + _noOfIterations);
                        Console.WriteLine("Success/Fail Analysis");
                        Console.WriteLine("Success Rate: {0}", ((RestartWithSideWay[1] * 100) / (float)_noOfIterations + '%').ToString(_formatter));

                        Console.WriteLine("Average Number of Steps When It Succeeds: " + (RestartWithSideWay[0] / _noOfIterations));
                        if ((RestartWithSideWay[2] / (float)_noOfIterations) > 0 && (RestartWithSideWay[2] / (float)_noOfIterations) < 1)
                            Console.WriteLine("Average Number of Restarts When It Succeeds: ~ 1");
                        else
                            Console.WriteLine("Average Number of Restarts When It Succeeds: " + (RestartWithSideWay[2] / (float)_noOfIterations));
                        //Console.ReadLine();
                        break;
                }
                Console.WriteLine("Do You want to continue with other algorithm?  Yes or No");
                UserDecision = Console.ReadLine().ToUpper();

            } while (UserDecision != "NO");           
        }
    }
}
