namespace passportcard
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger consoleLogger = new ConsoleLogger();

            consoleLogger.log("Insurance Rating System Starting...");

            var engine = new RatingEngine(consoleLogger);
            engine.Rate();

            if (engine.Rating > 0)
            {
                consoleLogger.log($"Rating: {engine.Rating}");
            }
            else
            {
                consoleLogger.log("No rating produced.");
            }

        }
    }
}
