using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallenge.Core.BusinessObjects
{
    public class Position
    {
        public int xPos
        {
            get; set;
        }

        public int yPos
        {
            get; set;
        }

        public bool Equals(Position testPosition)
        {
            if (this.xPos == testPosition.xPos && this.yPos == testPosition.yPos)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.xPos.ToString() + " " + this.yPos.ToString();
        }
    }
}

