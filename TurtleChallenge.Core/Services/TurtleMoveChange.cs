using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurtleChallenge.Core.BLL;
using TurtleChallenge.Core.BusinessObjects;
using TurtleChallenge.Core.enums;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Services
{
    public class TurtleMoveChange
    {
        private readonly Grid _grid;
        private Position _currentPosition;
        private Orientation _orientation;
        private List<Tile> _lstOfTiles;
        private ILogger _iLogger;
        public TurtleMoveChange(Grid grid, Position startPosition, ILogger iLogger)
        {
            _grid = grid;
            _currentPosition = startPosition;
            _iLogger = iLogger;
            _lstOfTiles = _grid.GridTiles.OfType<Tile>().ToList();
        }

        public MoveOutCome MoveNext(Position newPosition)
        {
            MoveOutCome moveOutCome = MoveOutCome.MoveSucessful;

            if (newPosition.Equals(_currentPosition))
            {
                return MoveOutCome.NoChange;
            }

            if (!CheckNewPostionWithInBoundry(newPosition))
            {
                return MoveOutCome.OutOfBuondsMove;
            }

            if (!ValidateNextMove(newPosition))
            {
                return MoveOutCome.IllegalMoveFromCurrentPosition;
            }

            if (HasHitMine(newPosition))
            {
                moveOutCome = MoveOutCome.HitMine;
            }

            if (HasHitExit(newPosition))
            {
                moveOutCome = MoveOutCome.HitExit;
            }

            DoMove(newPosition);
            return moveOutCome;
        }

        private void DoMove(Position newPosition)
        {
            Tile tile = _lstOfTiles.FirstOrDefault(
                pos => pos.TilePosition.xPos == _currentPosition.xPos && pos.TilePosition.yPos == _currentPosition.yPos);

            GetDirection(newPosition);
            DoOrientation(tile);

            MakeChangeToGrid(newPosition, tile);
            _lstOfTiles = _grid.GridTiles.OfType<Tile>().ToList();

            _currentPosition = newPosition;

        }

        private void MakeChangeToGrid(Position newPosition, Tile tile)
        {
            Tile tileNow = _lstOfTiles.FirstOrDefault(pos => pos.TilePosition.xPos == newPosition.xPos && pos.TilePosition.yPos == newPosition.yPos);
            tileNow.CurrentTurtle = tile.CurrentTurtle;

            Tile[,] tiles = new Tile[_grid.GridTiles.GetUpperBound(0) + 1, _grid.GridTiles.GetUpperBound(1) + 1];

            // Iterate through first
            for (int x = 0; x <= _grid.GridTiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= _grid.GridTiles.GetUpperBound(1); y++)
                {
                    Tile currentTile = _grid.GridTiles[x, y];

                    if (currentTile.TilePosition.Equals(newPosition))
                    {
                        currentTile.CurrentTurtle = tileNow.CurrentTurtle;
                    }
                    else if (currentTile.TilePosition.Equals(_currentPosition))
                    {
                        currentTile.CurrentTurtle = null;
                    }
                    tiles[x, y] = currentTile;
                }
            }

            _grid.GridTiles = tiles;
        }

        private void DoOrientation(Tile tile)
        {
            while (_orientation != tile.CurrentTurtle.orientation)
            {
                switch (tile.CurrentTurtle.orientation)
                {
                    case Orientation.North:
                        tile.CurrentTurtle.orientation = Orientation.East;
                        _iLogger.Info("Turtles orientation changed to east.");
                        break;
                    case Orientation.East:
                        tile.CurrentTurtle.orientation = Orientation.South;
                        _iLogger.Info("Turtles orientation changed to south.");
                        break;
                    case Orientation.South:
                        tile.CurrentTurtle.orientation = Orientation.West;
                        _iLogger.Info("Turtles orientation changed to west.");
                        break;
                    case Orientation.West:
                        tile.CurrentTurtle.orientation = Orientation.North;
                        _iLogger.Info("Turtles orientation changed to North.");
                        break;
                }
            }
        }

        private bool HasHitExit(Position newPosition)
        {
            return _lstOfTiles.FirstOrDefault(pos => pos.TilePosition.xPos == newPosition.xPos && pos.TilePosition.yPos == newPosition.yPos).HasExit;
        }

        private bool HasHitMine(Position newPosition)
        {
            return _lstOfTiles.FirstOrDefault(pos => pos.TilePosition.xPos == newPosition.xPos && pos.TilePosition.yPos == newPosition.yPos).HasMine;
        }

        /// <summary>
        /// Can only move one square to the North, East, south or west
        /// </summary>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        private bool ValidateNextMove(Position newPosition)
        {
            bool IsValid = false;
            if ((newPosition.xPos - _currentPosition.xPos == -1 &&
                newPosition.yPos - _currentPosition.yPos == 0) ||
               (newPosition.xPos - _currentPosition.xPos == 0 &&
                newPosition.yPos - _currentPosition.yPos == 1) ||
                (newPosition.xPos - _currentPosition.xPos == 0 &&
                newPosition.yPos - _currentPosition.yPos == -1) ||
                (newPosition.xPos - _currentPosition.xPos == 1 &&
                newPosition.yPos - _currentPosition.yPos == 0))
            {
                IsValid = true;
            };

            return IsValid;
        }

        private void GetDirection(Position newPosition)
        {
            _orientation = Orientation.NoOrientation;
            //move north
            if (newPosition.xPos - _currentPosition.xPos == -1 &&
                newPosition.yPos - _currentPosition.yPos == 0)
            {
                _orientation = Orientation.North;
            }

            //move east 
            if (newPosition.xPos - _currentPosition.xPos == 0 &&
                newPosition.yPos - _currentPosition.yPos == 1)
            {
                _orientation = Orientation.East;
            }

            //move east 
            if (newPosition.xPos - _currentPosition.xPos == 0 &&
                newPosition.yPos - _currentPosition.yPos == -1)
            {
                _orientation = Orientation.West;
            }

            //move south
            if (newPosition.xPos - _currentPosition.xPos == 1 &&
                newPosition.yPos - _currentPosition.yPos == 0)
            {
                _orientation = Orientation.South;
            }
        }

        private bool CheckNewPostionWithInBoundry(Position newPosition)
        {
            return _lstOfTiles.Any(pos => pos.TilePosition.xPos == newPosition.xPos && pos.TilePosition.yPos == newPosition.yPos);
        }
    }
}
