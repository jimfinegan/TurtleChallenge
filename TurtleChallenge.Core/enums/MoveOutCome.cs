using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallenge.Core.enums
{
    public enum MoveOutCome
    {
        NoChange,
        HitMine,
        HitExit,
        IllegalMoveFromCurrentPosition,
        OutOfBuondsMove,
        MoveSucessful
    }
}
