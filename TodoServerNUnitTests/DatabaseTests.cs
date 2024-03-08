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
            Assert.DoesNotThrowAsync(RemoveTestDataAsync);
            Assert.DoesNotThrowAsync(SaveTask); // SaveTaskTest
            RemoveTestDataSync();
        }

        [OneTimeTearDown]
        public void DatabaseCleanup()
        {
            RemoveTestDataSync();
        }

        private async void RemoveTestDataSync()
        {
            await RemoveTestDataAsync();
        }

        private async Task RemoveTestDataAsync()
        {
            string queryString = $"DELETE FROM {TestConstants.TEST_DB_TABLE_NAME}";

            await using SQLiteConnection connection = new SQLiteConnection(TestConstants.TEST_DB_CONNECTION_STRING);
            await connection.OpenAsync();
            await using SQLiteCommand command = new SQLiteCommand(queryString, connection);
            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
        
        private async Task SaveTask()
        {
            await RemoveTestDataAsync();

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

        [Test]
        public async Task GetTaskByIdTest()
        {
            await SaveTask();

            TodoTask? task = await DatabaseManager.GetTaskByIdFromDB(TestConstants.TEST_TASK_ID, TestConstants.TEST_DB_CONNECTION_STRING);
            Assert.That(task, Is.Not.Null);

            Assert.That(task.taskID, Is.EqualTo(TestConstants.TEST_TASK_ID));
            Assert.That(task.taskName, Is.EqualTo(TestConstants.TEST_TASK_NAME));
            Assert.That(task.listName, Is.EqualTo(TestConstants.TEST_TASK_LIST));
            Assert.That(task.dueDate, Is.EqualTo(TestConstants.GetTestDateType()));
            Assert.That(task.isImportant, Is.EqualTo(TestConstants.TEST_IS_IMPORTANT));
            Assert.That(task.isDone, Is.EqualTo(TestConstants.TEST_IS_DONE));
        }

        [Test]
        public async Task DeleteTaskByIdTest()
        {
            await SaveTask();

            await DatabaseManager.DeleteTaskByIdFromDB(TestConstants.TEST_TASK_ID, TestConstants.TEST_DB_CONNECTION_STRING);
            TodoTask? task = await DatabaseManager.GetTaskByIdFromDB(TestConstants.TEST_TASK_ID, TestConstants.TEST_DB_CONNECTION_STRING);
            Assert.That(task, Is.Null);
        }

        [Test]
        public async Task LoadTasksTest()
        {
            await SaveTask();

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
