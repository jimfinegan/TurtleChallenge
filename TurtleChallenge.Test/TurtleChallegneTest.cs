using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core;
using TurtleChallenge.Core.BLL;
using TurtleChallenge.Core.BusinessObjects;
using TurtleChallenge.Core.Configuration;
using TurtleChallenge.Core.Interfaces;
using Xunit;

namespace TrutleChallenge.Test
{
    public class TurtleChallegneTest
    {
        [Fact]
        public void TurtleChallegneStarted_WithInvalidMove_LogsInvalidNextMove()
        {
            IConfiguration configuration = new Configuration();

            configuration.GridDimension = new Position { xPos = 4, yPos = 4 };
            configuration.TurtlePosition = new Position { xPos = 1, yPos = 1 };
            List<Position> moves = new List<Position>();
            moves.Add(new Position { xPos = 2, yPos = 2 });
            configuration.Moves = moves;
            configuration.ExitPosition = new Position { xPos = 3, yPos = 3 };
            List<Position> mines = new List<Position>();
            configuration.MinesPosition = mines;
            ILogger logger = Substitute.For<ILogger>();

            Grid grid = new Grid();
            grid.GridTiles = new Tile[configuration.GridDimension.xPos, configuration.GridDimension.yPos];

            configuration.StartPostion = configuration.TurtlePosition;
            grid.Load(configuration.StartPostion, configuration.ExitPosition, configuration.MinesPosition);

            configuration.GameGrid = grid;

            TurtleChallenge.Core.TurtleChallenge turtleChallenge = new TurtleChallenge.Core.TurtleChallenge(configuration, logger);
            turtleChallenge.Start();

            logger.Received().InvalidNextMove(Arg.Any<string>());
        }


        [Fact]
        public void TurtleChallegneStarted_WithMineHit_LogsHasHitMine()
        {
            IConfiguration configuration = new Configuration();

            configuration.GridDimension = new Position { xPos = 4, yPos = 4 };
            configuration.TurtlePosition = new Position { xPos = 1, yPos = 1 };
            List<Position> moves = new List<Position>();
            moves.Add(new Position { xPos = 1, yPos = 2 });
            configuration.Moves = moves;
            configuration.ExitPosition = new Position { xPos = 3, yPos = 3 };
            List<Position> mines = new List<Position>();
            mines.Add(new Position { xPos = 1, yPos = 2 });
            configuration.MinesPosition = mines;
            ILogger logger = Substitute.For<ILogger>();

            Grid grid = new Grid();
            grid.GridTiles = new Tile[configuration.GridDimension.xPos, configuration.GridDimension.yPos];

            configuration.StartPostion = configuration.TurtlePosition;
            grid.Load(configuration.StartPostion, configuration.ExitPosition, configuration.MinesPosition);

            configuration.GameGrid = grid;

            TurtleChallenge.Core.TurtleChallenge turtleChallenge = new TurtleChallenge.Core.TurtleChallenge(configuration, logger);
            turtleChallenge.Start();

            logger.Received().HasHitMine(Arg.Any<string>());
        }



        [Fact]
        public void TurtleChallegneStarted_WithInBoundary_LogsNewPositionWithInBoundary()
        {
            IConfiguration configuration = new Configuration();

            configuration.GridDimension = new Position { xPos = 2, yPos = 2 };
            configuration.TurtlePosition = new Position { xPos = 1, yPos = 1 };
            List<Position> moves = new List<Position>();
            moves.Add(new Position { xPos = 1, yPos = 2 });

            moves.Add(new Position { xPos = 1, yPos = 3 });
            configuration.Moves = moves;
            configuration.ExitPosition = new Position { xPos = 3, yPos = 3 };
            List<Position> mines = new List<Position>();
            mines.Add(new Position { xPos = 1, yPos = 2 });
            configuration.MinesPosition = mines;
            ILogger logger = Substitute.For<ILogger>();

            Grid grid = new Grid();
            grid.GridTiles = new Tile[configuration.GridDimension.xPos, configuration.GridDimension.yPos];

            configuration.StartPostion = configuration.TurtlePosition;
            grid.Load(configuration.StartPostion, configuration.ExitPosition, configuration.MinesPosition);

            configuration.GameGrid = grid;

            TurtleChallenge.Core.TurtleChallenge turtleChallenge = new TurtleChallenge.Core.TurtleChallenge(configuration, logger);
            turtleChallenge.Start();

            logger.Received().NewPostionWithInBoundry(Arg.Any<string>());
        }

        [Fact]
        public void TurtleChallegneStarted_CorrectlyExits_LogsHasExited()
        {
            IConfiguration configuration = new Configuration();

            configuration.GridDimension = new Position { xPos = 2, yPos = 2 };
            configuration.TurtlePosition = new Position { xPos = 0, yPos = 0 };
            List<Position> moves = new List<Position>();
            moves.Add(new Position { xPos = 0, yPos = 1 });

            configuration.Moves = moves;
            configuration.ExitPosition = new Position { xPos = 0, yPos = 1 };
            List<Position> mines = new List<Position>();
            mines.Add(new Position { xPos = 1, yPos = 1 });
            configuration.MinesPosition = mines;
            ILogger logger = Substitute.For<ILogger>();

            Grid grid = new Grid();
            grid.GridTiles = new Tile[configuration.GridDimension.xPos, configuration.GridDimension.yPos];

            configuration.StartPostion = configuration.TurtlePosition;
            grid.Load(configuration.StartPostion, configuration.ExitPosition, configuration.MinesPosition);

            configuration.GameGrid = grid;

            TurtleChallenge.Core.TurtleChallenge turtleChallenge = new TurtleChallenge.Core.TurtleChallenge(configuration, logger);
            turtleChallenge.Start();

            logger.Received().HasHitExit(Arg.Any<string>());
        }

        
        [Fact]
        public void TurtleChallegneStarted_HasHitExitWithMovesLeft_LogsHitExitWithMovesLeft()
        {
            IConfiguration configuration = new Configuration();

            configuration.GridDimension = new Position { xPos = 2, yPos = 2 };
            configuration.TurtlePosition = new Position { xPos = 0, yPos = 0 };
            List<Position> moves = new List<Position>();
            moves.Add(new Position { xPos = 0, yPos = 1 });
            moves.Add(new Position { xPos = 1, yPos = 1 });

            configuration.Moves = moves;
            configuration.ExitPosition = new Position { xPos = 0, yPos = 1 };
            List<Position> mines = new List<Position>();
            mines.Add(new Position { xPos = 1, yPos = 1 });
            configuration.MinesPosition = mines;
            ILogger logger = Substitute.For<ILogger>();

            Grid grid = new Grid();
            grid.GridTiles = new Tile[configuration.GridDimension.xPos, configuration.GridDimension.yPos];

            configuration.StartPostion = configuration.TurtlePosition;
            grid.Load(configuration.StartPostion, configuration.ExitPosition, configuration.MinesPosition);

            configuration.GameGrid = grid;

            TurtleChallenge.Core.TurtleChallenge turtleChallenge = new TurtleChallenge.Core.TurtleChallenge(configuration, logger);
            turtleChallenge.Start();

            logger.Received().HasHitExitButMoveLeft(Arg.Any<string>());
        }
    }
}

