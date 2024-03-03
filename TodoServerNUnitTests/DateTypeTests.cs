using TodoServer;

namespace TodoServerNUnitTests
{
    internal class DateTypeTests
    {
        [Test]
        public void DateTypeToStringTest()
        {
            DateType dateType = new DateType();
            dateType.day = TestConstants.TEST_DUE_DATE_DAY;
            dateType.month = TestConstants.TEST_DUE_DATE_MONTH;
            dateType.year = TestConstants.TEST_DUE_DATE_YEAR;

            string testString = $"0{TestConstants.TEST_DUE_DATE_MONTH}/0{TestConstants.TEST_DUE_DATE_DAY}/{TestConstants.TEST_DUE_DATE_YEAR}";
            Assert.That(dateType.ToString(), Is.EqualTo(testString));

            dateType.day = TestConstants.TEST_DUE_DATE_DAY_TWO_DIGITS;
            dateType.month = TestConstants.TEST_DUE_DATE_MONTH_TWO_DIGITS;

            testString = $"{TestConstants.TEST_DUE_DATE_MONTH_TWO_DIGITS}/{TestConstants.TEST_DUE_DATE_DAY_TWO_DIGITS}/{TestConstants.TEST_DUE_DATE_YEAR}";
            Assert.That(dateType.ToString(), Is.EqualTo(testString));
        }

        [Test]
        public void StringToDateTypeTest()
        {
            string testString = $"0{TestConstants.TEST_DUE_DATE_MONTH}/0{TestConstants.TEST_DUE_DATE_DAY}/{TestConstants.TEST_DUE_DATE_YEAR}";
            DateType dateType = DateType.GetDateTypeFromString(testString);

            Assert.That(dateType.day, Is.EqualTo(TestConstants.TEST_DUE_DATE_DAY));
            Assert.That(dateType.month, Is.EqualTo(TestConstants.TEST_DUE_DATE_MONTH));
            Assert.That(dateType.year, Is.EqualTo(TestConstants.TEST_DUE_DATE_YEAR));

            testString = $"{TestConstants.TEST_DUE_DATE_MONTH_TWO_DIGITS}/{TestConstants.TEST_DUE_DATE_DAY_TWO_DIGITS}/{TestConstants.TEST_DUE_DATE_YEAR}";
            dateType = DateType.GetDateTypeFromString(testString);

            Assert.That(dateType.day, Is.EqualTo(TestConstants.TEST_DUE_DATE_DAY_TWO_DIGITS));
            Assert.That(dateType.month, Is.EqualTo(TestConstants.TEST_DUE_DATE_MONTH_TWO_DIGITS));
            Assert.That(dateType.year, Is.EqualTo(TestConstants.TEST_DUE_DATE_YEAR));
        }
    }
}
