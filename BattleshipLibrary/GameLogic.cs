using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleshipLibrary.Models;

namespace BattleshipLibrary
{
    public static class GameLogic
    {


        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string> { "A", "B", "C", "D", "E" };
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
        }
        public static int GetShotCount(PlayerInfoModel winner)
        {
            throw new NotImplementedException();
        }
        public static bool PlaceShip(PlayerInfoModel model, string? location)
        {
            throw new NotImplementedException();
        }

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }
            return isActive;
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        public static (string col, int row) SplitShotIntoRowAndColumn(string shot)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateShot(PlayerInfoModel activePlayer, string col, int row)
        {
            throw new NotImplementedException();
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static void MarkShotResult(PlayerInfoModel activePlayer, string row, int column, bool isAHit)
        {
            throw new NotImplementedException();
        }
    }
}