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
            LogWarning("Warning", warningMessage);
        }

        public static void LogWarning(string warningName, string warningMessage)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ChangeConsoleColor(ConsoleColor.Black, warningColor);
            Console.Write($"[{warningName}]");
            ChangeConsoleColor(warningColor, defaultBackgroundColor);
            Console.WriteLine($" {warningMessage}");
            Console.ResetColor();
        }

        public static void LogError(string errorMessage)
        {
            LogError("Error", errorMessage);
        }

        public static void LogError(string errorName, string errorMessage)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ChangeConsoleColor(ConsoleColor.Black, errorColor);
            Console.Write($"[{errorName}]");
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
