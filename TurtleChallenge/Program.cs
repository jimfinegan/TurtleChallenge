using System;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Configuration;
using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Core.Logger;

namespace TrutleChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new Configuration();
            ILogger feedBackLogger = new FeedBackDelegateLogger(WriteOutToConsole);

            Configuration.LoadConfiguration();
            TurtleChallenge.Core.TurtleChallenge turtleChallenge = new TurtleChallenge.Core.TurtleChallenge(Configuration, feedBackLogger);

            turtleChallenge.Start();

            Console.Write("Press any key to exit.");
            Console.Read();
        }

        private static void WriteOutToConsole(string TextOutput)
        {
            Console.Write(TextOutput + Environment.NewLine);
            Console.Write(Environment.NewLine);
        }
    }
}
