using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurtleChallenge.Core.BusinessObjects;
using TurtleChallenge.Core.enums;

namespace TurtleChallenge.Core.BLL
{

    public class Grid
    {
        public Tile[,] GridTiles
        {
            get; set;
        }

        public void Load(Position posTurtle, Position posExit, List<Position> posMines)
        {
            ValidateInput(posTurtle, posExit, posMines);

            for (int x = 0; x <= GridTiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= GridTiles.GetUpperBound(1); y++)
                {
                    Tile currentTile = new Tile { TilePosition = new Position { xPos = x, yPos = y } };

                    if (currentTile.TilePosition.Equals(posTurtle))
                    {
                        currentTile.CurrentTurtle = new Turtle { CurrentPosition = currentTile.TilePosition, orientation = Orientation.North };
                    }
                    else if (currentTile.TilePosition.Equals(posExit))
                    {
                        currentTile.HasExit = true;
                    }

                    if (posMines.Any(pos => pos.xPos == currentTile.TilePosition.xPos && pos.yPos == currentTile.TilePosition.yPos))
                    {
                        currentTile.HasMine = true;
                    }

                    GridTiles[x, y] = currentTile;
                }
            }
        }

        private void ValidateInput(Position posTurtle, Position posExit, List<Position> posMines)
        {
            if (posTurtle.Equals(posExit))
            {
                throw new Exception("Turtle and exit position cannot be the same");
            }

            Position firstMinesPOsition = posMines.FirstOrDefault();

            if (firstMinesPOsition != null && firstMinesPOsition.Equals(posTurtle))
            {
                throw new Exception("Turtle cannot start on a mine");
            }

            if (posMines.Contains(posExit))
            {
                throw new Exception("exit cannot be on a mine");
            }
        }
    }
}
