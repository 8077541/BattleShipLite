using System;
using System.Collections.Generic;
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
            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");
            Console.ReadLine();
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

