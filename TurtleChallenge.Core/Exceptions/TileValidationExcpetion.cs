using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallenge.Core.Exceptions
{
    public class TileValidationExcpetion : Exception
    {
        public TileValidationExcpetion(string message) : base(message)
        {
        }
    }
}
