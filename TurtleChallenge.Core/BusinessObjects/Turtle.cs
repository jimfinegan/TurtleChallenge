using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core.enums;

namespace TurtleChallenge.Core.BusinessObjects
{
    public class Turtle
    {
        public Orientation orientation
        {
            get; set;
        }

        public Position CurrentPosition
        {
            get; set;
        }
    }
}
