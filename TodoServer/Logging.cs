namespace TodoServer
{
    public static class Logging
    {
        private const ConsoleColor warningColor = ConsoleColor.Yellow;
        private const ConsoleColor errorColor = ConsoleColor.Red;

        public static void Log(string logMessage)
        {
            Console.ResetColor();
            Console.WriteLine($"[Log] {logMessage}");
        }

        public static void LogWarning(string warningMessage)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ChangeConsoleColor(ConsoleColor.Black, warningColor);
            Console.Write("[Warning]");
            ChangeConsoleColor(warningColor, defaultBackgroundColor);
            Console.WriteLine($" {warningMessage}");
            Console.ResetColor();
        }

        public static void LogError(string errorMessage)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ChangeConsoleColor(ConsoleColor.Black, errorColor);
            Console.Write("[Error]");
            ChangeConsoleColor(errorColor, defaultBackgroundColor);
            Console.WriteLine($" {errorMessage}");
            Console.ResetColor();
        }

        private static void ChangeConsoleColor(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            if (!(foregroundColor == Console.ForegroundColor))
            {
                Console.ForegroundColor = foregroundColor;
            }

            if (!(backgroundColor == Console.BackgroundColor))
            {
                Console.BackgroundColor = backgroundColor;
            }
        }
    }
}
