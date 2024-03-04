namespace TodoServer
{
    public static class Logging
    {
        private const string DEFAULT_LOG_NAME = "Log";
        private const string DEFAULT_WARNING_NAME = "Warning";
        private const string DEFAULT_ERROR_NAME = "Error";

        private const ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor ERROR_COLOR = ConsoleColor.Red;

        public static void Log(string logMessage, string logName = DEFAULT_LOG_NAME)
        {
            Console.ResetColor();
            Console.WriteLine($"[{logName}] {logMessage}");
        }

        public static void LogWarning(string warningMessage, string warningName = DEFAULT_WARNING_NAME)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ChangeConsoleColor(ConsoleColor.Black, WARNING_COLOR);
            Console.Write($"[{warningName}]");
            ChangeConsoleColor(WARNING_COLOR, defaultBackgroundColor);
            Console.WriteLine($" {warningMessage}");
            Console.ResetColor();
        }

        public static void LogError(string errorMessage, string errorName = DEFAULT_ERROR_NAME)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ChangeConsoleColor(ConsoleColor.Black, ERROR_COLOR);
            Console.Write($"[{errorName}]");
            ChangeConsoleColor(ERROR_COLOR, defaultBackgroundColor);
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
