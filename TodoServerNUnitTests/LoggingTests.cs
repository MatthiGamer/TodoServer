using TodoServer;

namespace TodoServerNUnitTests
{
    internal class LoggingTests
    {
        [Test]
        public void LoggingMethodsTest()
        {
            StringWriter testConsole;
            
            // Test logs
            testConsole = GetNewConsoleOutput();
            Assert.DoesNotThrow(() => Logging.Log(TestConstants.TEST_LOG));
            Assert.That(testConsole.ToString(), Is.EqualTo($"[{TestConstants.TEST_DEFAULT_LOG_NAME}] {TestConstants.TEST_LOG}{Environment.NewLine}"));

            testConsole = GetNewConsoleOutput();
            Assert.DoesNotThrow(() => Logging.Log(TestConstants.TEST_LOG, TestConstants.TEST_LOG_NAME));
            Assert.That(testConsole.ToString(), Is.EqualTo($"[{TestConstants.TEST_LOG_NAME}] {TestConstants.TEST_LOG}{Environment.NewLine}"));

            // Test warnings
            testConsole = GetNewConsoleOutput();
            Assert.DoesNotThrow(() => Logging.LogWarning(TestConstants.TEST_WARNING));
            Assert.That(testConsole.ToString(), Is.EqualTo($"[{TestConstants.TEST_DEFAULT_WARNING_NAME}] {TestConstants.TEST_WARNING}{Environment.NewLine}"));

            testConsole = GetNewConsoleOutput();
            Assert.DoesNotThrow(() => Logging.LogWarning(TestConstants.TEST_WARNING, TestConstants.TEST_WARNING_NAME));
            Assert.That(testConsole.ToString(), Is.EqualTo($"[{TestConstants.TEST_WARNING_NAME}] {TestConstants.TEST_WARNING}{Environment.NewLine}"));

            // Test errors
            testConsole = GetNewConsoleOutput();
            Assert.DoesNotThrow(() => Logging.LogError(TestConstants.TEST_ERROR));
            Assert.That(testConsole.ToString(), Is.EqualTo($"[{TestConstants.TEST_DEFAULT_ERROR_NAME}] {TestConstants.TEST_ERROR}{Environment.NewLine}"));

            testConsole = GetNewConsoleOutput();
            Assert.DoesNotThrow(() => Logging.LogError(TestConstants.TEST_ERROR, TestConstants.TEST_ERROR_NAME));
            Assert.That(testConsole.ToString(), Is.EqualTo($"[{TestConstants.TEST_ERROR_NAME}] {TestConstants.TEST_ERROR}{Environment.NewLine}"));

            ResetConsoleOutput();
        }

        private StringWriter GetNewConsoleOutput()
        {
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            return stringWriter;
        }

        private void ResetConsoleOutput()
        {
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
    }
}
