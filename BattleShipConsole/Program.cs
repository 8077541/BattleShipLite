using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;
using BattleshipLibrary.Models;

namespace BattleShipConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                //Display grid from activePlayer on where they fired
                DisplayShotGrid(activePlayer);

                //Ask activePlayer for a shot 
                //Determine if it is a valid shot
                //Determine shot results
                RecordPlayerShot(activePlayer, opponent);

                //Determine if the game should continue
                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                //If over, set activePlayer as winner
                if (doesGameContinue)
                {
                    //swap with temp var

                    // PlayerInfoModel temp = opponent;
                    // opponent = activePlayer;
                    // activePlayer = temp;

                    //swap with Tuple
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

                //Else, swap positions (active player = opponent)



            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
            Console.WriteLine($"{winner.UsersName} took {GameLogic.GetShotCount(winner)} shots.");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = "";
            int col = 0;
            do
            {
                string shot = AskForShot();
                (row, col) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, col);

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location. Please try again.");
                }

            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, col);
            //Determine shot results
            GameLogic.MarkShotResult(activePlayer, row, col, isAHit);
            //Record Results
        }

        private static string AskForShot()
        {
            Console.Write("Please enter your shot selection: ");
            return Console.ReadLine();
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    currentRow = gridSpot.SpotLetter;
                    Console.WriteLine();
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }

            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to BattleShip");
            Console.WriteLine("created by Dominik Janiak");
            Console.WriteLine("");
        }
        private static PlayerInfoModel CreatePlayer(string name)
        {
            PlayerInfoModel output = new PlayerInfoModel();
            System.Console.WriteLine($"Player information for {name}");

            //Ask the user for their name
            output.UsersName = AskForUsersName();

            //Load up the shot grid
            GameLogic.InitializeGrid(output);

            //Ask user for 5 ships placement
            PlaceShips(output);

            //Clear
            Console.Clear();

            return output;
        }

        private static string AskForUsersName()

        {
            Console.WriteLine("What is your name?: ");
            string output = Console.ReadLine();
            return output;
        }
        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.WriteLine($"Where do you want to place ship number {model.ShipLocations.Count + 1}: ");
                string location = Console.ReadLine();
                bool isValidLocation = GameLogic.PlaceShip(model, location);
                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }
            } while (model.ShipLocations.Count < 5);
        }
    }
}

