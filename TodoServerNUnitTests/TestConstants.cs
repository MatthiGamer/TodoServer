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
