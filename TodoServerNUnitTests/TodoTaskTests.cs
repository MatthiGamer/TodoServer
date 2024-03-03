using TodoServer;

namespace TodoServerNUnitTests
{
    internal class TodoTaskTests
    {
        [Test]
        public void TodoTaskToStringTest()
        {
            TodoTask task = new TodoTask(
                TestConstants.TEST_TASK_ID,
                TestConstants.TEST_TASK_NAME,
                TestConstants.TEST_TASK_LIST,
                TestConstants.GetTestDateType(),
                TestConstants.TEST_IS_IMPORTANT,
                TestConstants.TEST_IS_DONE
            );

            string testString = $"{{ID: {TestConstants.TEST_TASK_ID}, " +
                                $"Name: {TestConstants.TEST_TASK_NAME}, " +
                                $"List: {TestConstants.TEST_TASK_LIST}, " +
                                $"Due to: {TestConstants.GetTestDateType()}, " +
                                $"isImportant: {TestConstants.TEST_IS_IMPORTANT}, " +
                                $"isDone: {TestConstants.TEST_IS_DONE}}}";

            Assert.That(task.ToString(), Is.EqualTo(testString));

            task.dueDate = null;

            testString = $"{{ID: {TestConstants.TEST_TASK_ID}, " +
                                $"Name: {TestConstants.TEST_TASK_NAME}, " +
                                $"List: {TestConstants.TEST_TASK_LIST}, " +
                                "Due to: End of time, " +
                                $"isImportant: {TestConstants.TEST_IS_IMPORTANT}, " +
                                $"isDone: {TestConstants.TEST_IS_DONE}}}";

            Assert.That(task.ToString(), Is.EqualTo(testString));
        }
    }
}
