namespace passportcard
{
    public interface ILogger
    {
        void log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
