using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core.BLL;
using TurtleChallenge.Core.BusinessObjects;

namespace TurtleChallenge.Core.Interfaces
{

    public interface IConfiguration
    {
        Position TurtlePosition
        {
            get; set;
        }

        Position ExitPosition
        {
            get; set;
        }

        List<Position> MinesPosition
        {
            get; set;
        }

        List<Position> Moves
        {
            get; set;
        }

        Position GridDimension
        {
            get; set;
        }

        Position StartPostion
        {
            get; set;
        }

        Grid GameGrid
        {
            get; set;
        }

        void LoadConfiguration();
    }
}

