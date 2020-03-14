using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Logger
{
    public class FeedBackDelegateLogger : ILogger
    {
        public FeedBackDelegateLogger(FeedbackMessageDelegate feedbackMessageDelegate)
        {
            this.feedback = feedbackMessageDelegate;
        }

        public void Info(string message)
        {
            LogMessage(message);
        }

        public void Error(string message)
        {
            LogMessage(message);
        }

        private void LogMessage(string message)
        {
            this.feedback(message);
        }


        public void NewPostionWithInBoundry(string message)
        {
            this.feedback(message);
        }

        public void InvalidNextMove(string message)
        {
            this.feedback(message);
        }

        public void HasHitMine(string message)
        {
            this.feedback(message);
        }

        public void HasHitExit(string message)
        {
            this.feedback(message);
        }

        public void HasHitExitButMoveLeft(string message)
        {
            this.feedback(message);
        }

        private FeedbackMessageDelegate feedback;
    }
}
