using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core.Exceptions;

namespace TurtleChallenge.Core.BusinessObjects
{
    public class Tile
    {
        public Position TilePosition
        {
            get; set;
        }

        public bool HasMine
        {
            get; set;
        }

        public bool HasExit
        {
            get; set;
        }

        public Turtle CurrentTurtle
        {
            get; set;
        }
        public void Validate()
        {
            if (HasExit && HasExit)
            {
                throw new TileValidationExcpetion("A tile can not contain both a mine and an exit");
            }
        }
    }
}
