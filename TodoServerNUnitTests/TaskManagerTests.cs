using Newtonsoft.Json;
using TodoServer;

namespace TodoServerNUnitTests
{
    public class TaskManagerTests
    {
        // String constants
        private const string TEST_TASK_ID = "ID";
        private const string TEST_TASK_NAME = "Task";
        private const string TEST_TASK_LIST = "Todo";

        // Date constants
        private const int TEST_DUE_DATE_DAY = 1;
        private const int TEST_DUE_DATE_MONTH = 1;
        private const int TEST_DUE_DATE_YEAR = 2024;

        // Boolean constants
        private const bool TEST_IS_IMPORTANT = false;
        private const bool TEST_IS_DONE = false;

        [Test]
        public void TaskManagerInstanceTest()
        {
            Assert.IsNotNull(TaskManager.GetInstance());
        }

        [Test]
        public void TaskManagerSingletonEqualsTest()
        {
            TaskManager taskManager = TaskManager.GetInstance();
            Assert.That(taskManager, Is.SameAs(TaskManager.GetInstance()));
        }

        [Test]
        public void TaskManagerGetTaskByIDTest()
        {
            SaveTestTask();

            string savedTasks = TaskManager.GetInstance().GetTasks();
            TodoTask[]? tasks = JsonConvert.DeserializeObject<TodoTask[]>(savedTasks);
            if (tasks == null) Assert.Fail("The deserialization failed.");
            if (tasks!.Length < 1) Assert.Fail("There are no tasks saved.");

            TodoTask? savedTask = tasks[0];
            if (savedTask == null) Assert.Fail("The saved task ist null.");

            TodoTask? task = TaskManager.GetInstance().GetTaskById(TEST_TASK_ID);
            if (task == null) Assert.Fail("Task not found by GetTaskById().");

            Assert.That(task!.taskID, Is.EqualTo(savedTask!.taskID));
        }

        [Test]
        public void TaskManagerGetTasksToJSONTest()
        {
            SaveTestTask();

            string savedTasks = TaskManager.GetInstance().GetTasks();
            StringAssert.AreEqualIgnoringCase(
                $@"[{{""taskID"":""ID"",""taskName"":""Task"",""taskList"":""Todo"",""dueDate"":{{""day"":{TEST_DUE_DATE_DAY},""month"":{TEST_DUE_DATE_MONTH},""year"":{TEST_DUE_DATE_YEAR}}},""isImportant"":false,""isDone"":false}}]",
                savedTasks);
        }

        [Test]
        public void TaskManagerGetTasksFromJSONTest()
        {
            SaveTestTask();

            string savedTasks = TaskManager.GetInstance().GetTasks();
            TodoTask[]? tasks = JsonConvert.DeserializeObject<TodoTask[]>(savedTasks);
            TodoTask newTask = tasks[0];

            Assert.That(newTask.taskID, Is.EqualTo(TEST_TASK_ID));
            Assert.That(newTask.taskName, Is.EqualTo(TEST_TASK_NAME));
            Assert.That(newTask.taskList, Is.EqualTo(TEST_TASK_LIST));
            Assert.That(newTask.dueDate, Is.EqualTo(GetTestDateType()));
            Assert.That(newTask.isImportant, Is.EqualTo(TEST_IS_IMPORTANT));
            Assert.That(newTask.isDone, Is.EqualTo(TEST_IS_DONE));
        }

        private static void SaveTestTask()
        {
            if (TaskManager.GetInstance().GetTaskById(TEST_TASK_ID) != null) return;

            TodoTask task = new TodoTask(
                TEST_TASK_ID,
                TEST_TASK_NAME,
                TEST_TASK_LIST,
                GetTestDateType(),
                TEST_IS_IMPORTANT,
                TEST_IS_DONE
            );

            TaskManager.GetInstance().SaveTask(task);
        }

        private static DateType GetTestDateType() => new DateType() { day = TEST_DUE_DATE_DAY, month = TEST_DUE_DATE_MONTH, year = TEST_DUE_DATE_YEAR };
    }
}