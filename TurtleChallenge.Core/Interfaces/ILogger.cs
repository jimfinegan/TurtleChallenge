using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallenge.Core.Interfaces
{
    public interface ILogger
    {

        #region PUBLIC METHODS

        void Info(string message);

        void Error(string message);

        void NewPostionWithInBoundry(string message);

        void InvalidNextMove(string message);

        void HasHitMine(string message);

        void HasHitExit(string message);

        void HasHitExitButMoveLeft(string message);
        #endregion
    }
}
