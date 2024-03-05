using System.Data.SQLite;
using TodoServer;

namespace TodoServerNUnitTests
{
    internal class DatabaseTests
    {
        [OneTimeSetUp]
        public void DatabaseSetup()
        {
            Assert.That(File.Exists(Path.GetFullPath(TestConstants.TEST_DB_PATH)), Is.True);
            TestContext.WriteLine(Path.GetFullPath(TestConstants.TEST_DB_PATH));
        }

        [OneTimeTearDown]
        public void DatabaseCleanup()
        {
            RemoveTestData();
        }

        private async void RemoveTestData()
        {
            string queryString = $"DELETE FROM {TestConstants.TEST_DB_TABLE_NAME}";

            await using SQLiteConnection connection = new SQLiteConnection(TestConstants.TEST_DB_CONNECTION_STRING);
            await connection.OpenAsync();
            await using SQLiteCommand command = new SQLiteCommand(queryString, connection);

            await connection.CloseAsync();
        }

        [Test, Order(1)]
        public async Task SaveTaskTest()
        {
            RemoveTestData();

            TodoTask task = new TodoTask(
                TestConstants.TEST_TASK_ID,
                TestConstants.TEST_TASK_NAME,
                TestConstants.TEST_TASK_LIST,
                TestConstants.GetTestDateType(),
                TestConstants.TEST_IS_IMPORTANT,
                TestConstants.TEST_IS_DONE
            );

            await DatabaseManager.SaveTask(task, TestConstants.TEST_DB_CONNECTION_STRING);
        }

        [Test, Order(2)]
        public async Task LoadTasksTest()
        {
            List<TodoTask> tasks = await DatabaseManager.GetTasksFromDB(TestConstants.TEST_DB_CONNECTION_STRING);
            TodoTask savedTask = tasks.ToArray()[0];

            Assert.That(savedTask.taskID, Is.EqualTo(TestConstants.TEST_TASK_ID));
            Assert.That(savedTask.taskName, Is.EqualTo(TestConstants.TEST_TASK_NAME));
            Assert.That(savedTask.listName, Is.EqualTo(TestConstants.TEST_TASK_LIST));
            Assert.That(savedTask.dueDate, Is.EqualTo(TestConstants.GetTestDateType()));
            Assert.That(savedTask.isImportant, Is.EqualTo(TestConstants.TEST_IS_IMPORTANT));
            Assert.That(savedTask.isDone, Is.EqualTo(TestConstants.TEST_IS_DONE));
        }
    }
}
