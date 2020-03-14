using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core.BLL;
using TurtleChallenge.Core.BusinessObjects;
using Xunit;

namespace TrutleChallenge.Test.BLL
{
    public class GridTest
    {
        [Fact]
        public void Load_StartAndExitPositionSame_ExceptionThrown()
        {
            Grid grid = new Grid();
            Position postionStart = new Position { xPos = 1, yPos = 1 };
            Position postionEnd = new Position { xPos = 1, yPos = 1 };

            Assert.Throws<Exception>(() => grid.Load(postionStart, postionEnd, null));
        }


        [Fact]
        public void Load_StartAndFirstMinePositionSame_ExceptionThrown()
        {
            Grid grid = new Grid();
            Position postionStart = new Position { xPos = 1, yPos = 1 };
            Position postionEnd = new Position { xPos = 2, yPos = 2 };
            List<Position> postionMines = new List< Position> () { new Position { xPos = 1, yPos = 1 } };

            Assert.Throws<Exception>(() => grid.Load(postionStart, postionEnd, postionMines));
        }


        [Fact]
        public void Load_ExitPositionHasAMine_ExceptionThrown()
        {
            Grid grid = new Grid();
            Position postionStart = new Position { xPos = 1, yPos = 1 };
            Position postionEnd = new Position { xPos = 2, yPos = 2 };
            List<Position> postionMines = new List<Position>() { new Position { xPos = 1, yPos = 1 }, new Position { xPos = 2, yPos = 2 } };

            Assert.Throws<Exception>(() => grid.Load(postionStart, postionEnd, postionMines));
        }
    }
}
