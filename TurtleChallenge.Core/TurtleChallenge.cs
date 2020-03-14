using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core.BLL;
using TurtleChallenge.Core.BusinessObjects;
using TurtleChallenge.Core.enums;
using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Core.Logger;
using TurtleChallenge.Core.Services;

namespace TurtleChallenge.Core
{
    public class TurtleChallenge
    {
        private List<Position> _moves;
        private Position _startPosition;
        private IConfiguration _iConfiguration;
        private ILogger _ilogger;
        private TurtleMoveChange _turtleMoveChange;

        public TurtleChallenge(IConfiguration iConfiguration, ILogger ilogger)
        {
            _iConfiguration = iConfiguration;
            _ilogger = ilogger;
        }

        public void Start()
        {
            _turtleMoveChange = new TurtleMoveChange(_iConfiguration.GameGrid, _iConfiguration.StartPostion, _ilogger);
            Position lastPosition = _iConfiguration.StartPostion;

            _ilogger.Info("Game started at position " + lastPosition.ToString());

            ExecuteMoves(lastPosition);
        }

        private void ExecuteMoves(Position lastPosition)
        {
            int count = 1;
            foreach (Position movePosition in _iConfiguration.Moves)
            {
                MoveOutCome moveOutCome = _turtleMoveChange.MoveNext(movePosition);

                switch (moveOutCome)
                {
                    case MoveOutCome.IllegalMoveFromCurrentPosition:
                        _ilogger.InvalidNextMove("Illegal move from position " + lastPosition.ToString() + " to position " + movePosition);
                        _ilogger.Info("Game over");
                        return;
                    case MoveOutCome.OutOfBuondsMove:
                        _ilogger.NewPostionWithInBoundry("Position " + movePosition.ToString() + " is out of bounds.");
                        _ilogger.Info("Game over");
                        return;
                    case MoveOutCome.HitExit:
                        if (count == _iConfiguration.Moves.Count)
                        {
                            _ilogger.HasHitExit("Exit position hit at " + movePosition.ToString());
                            _ilogger.Info("Game over");
                        }
                        else
                        {
                            _ilogger.HasHitExitButMoveLeft("Exit position hit at " + movePosition.ToString() + " but there are still moves left");
                            _ilogger.Info("Game over");
                        }
                        return;
                    case MoveOutCome.HitMine:
                        _ilogger.HasHitMine("Mines Hit at position " + movePosition.ToString());
                        _ilogger.Info("Game over");
                        return;
                    case MoveOutCome.MoveSucessful:
                        _ilogger.Info("Move to position " + lastPosition.ToString() + " to " + movePosition.ToString() + " was sucessful");
                        break;
                }

                lastPosition = movePosition;
                count++;
            }
        }
    }
}
