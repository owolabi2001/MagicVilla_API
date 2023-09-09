namespace MagicVilla_VillaAPI.loggingh
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.WriteLine("Error - " + message);
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.WriteLine("Info - " + message);
            }
        }
    }
}
