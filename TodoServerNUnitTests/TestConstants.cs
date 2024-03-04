using TodoServer;

namespace TodoServerNUnitTests
{
    internal static class TestConstants
    {
        // String constants
        public const string TEST_SAVE_TASK_ID = "ID_Save";

        public const string TEST_TASK_ID = "ID";
        public const string TEST_TASK_NAME = "Task";
        public const string TEST_TASK_LIST = "Todo";

        public const string TEST_LOG = "Test log";
        public const string TEST_WARNING = "Test warning";
        public const string TEST_ERROR = "Test error";

        public const string TEST_LOG_NAME = "Info";
        public const string TEST_WARNING_NAME = "TestWarning";
        public const string TEST_ERROR_NAME = "TestError";

        public const string TEST_DEFAULT_LOG_NAME = "Log";
        public const string TEST_DEFAULT_WARNING_NAME = "Warning";
        public const string TEST_DEFAULT_ERROR_NAME = "Error";

        // Date constants
        public const int TEST_DUE_DATE_DAY = 1;
        public const int TEST_DUE_DATE_DAY_TWO_DIGITS = 24;
        public const int TEST_DUE_DATE_MONTH = 1;
        public const int TEST_DUE_DATE_MONTH_TWO_DIGITS = 12;
        public const int TEST_DUE_DATE_YEAR = 2024;

        // Boolean constants
        public const bool TEST_IS_IMPORTANT = false;
        public const bool TEST_IS_DONE = false;

        public static DateType GetTestDateType() => new DateType() { day = TEST_DUE_DATE_DAY, month = TEST_DUE_DATE_MONTH, year = TEST_DUE_DATE_YEAR };
    }
}
