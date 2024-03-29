using Newtonsoft.Json;
using TodoServer;

namespace TodoServerNUnitTests
{
    public class TaskManagerTests
    {
        [SetUp]
        public void Setup()
        {
            TodoTask task = new TodoTask(
                TestConstants.TEST_TASK_ID,
                TestConstants.TEST_TASK_NAME,
                TestConstants.TEST_TASK_LIST,
                TestConstants.GetTestDateType(),
                TestConstants.TEST_IS_IMPORTANT,
                TestConstants.TEST_IS_DONE
            );

            Assert.DoesNotThrow(() => TaskManager.GetInstance().SaveTask(task));
        }

        [TearDown]
        public void TearDown()
        {
            Assert.DoesNotThrow(() => TaskManager.GetInstance().DeleteTaskById(TestConstants.TEST_TASK_ID));
        }

        [Test]
        public void TaskManagerDeleteTaskTest()
        {
            TodoTask savedTask = GetSavedTask();

            TaskManager.GetInstance().DeleteTaskById(savedTask.taskID);

            string savedTasks = TaskManager.GetInstance().GetTasks();
            Assert.That(savedTasks, Is.EqualTo("[]"));
        }

        [Test]
        public void TaskManagerGetTaskByIDTest()
        {
            TodoTask savedTask = GetSavedTask();

            TodoTask? task = TaskManager.GetInstance().GetTaskById(TestConstants.TEST_TASK_ID);
            if (task == null) Assert.Fail("Task not found by GetTaskById().");

            Assert.That(task!.taskID, Is.EqualTo(savedTask!.taskID));
        }

        [Test]
        public void TaskManagerGetTasksToJSONTest()
        {
            string savedTasks = TaskManager.GetInstance().GetTasks();
            Assert.That(savedTasks, Is.EqualTo(
                $@"[{{""taskID"":""ID"",""taskName"":""Task"",""listName"":""Todo"",""dueDate"":{{""day"":{TestConstants.TEST_DUE_DATE_DAY},""month"":{TestConstants.TEST_DUE_DATE_MONTH},""year"":{TestConstants.TEST_DUE_DATE_YEAR}}},""isImportant"":false,""isDone"":false}}]"
                )
            );
        }

        [Test]
        public void TaskManagerGetTasksFromJSONTest()
        {
            TodoTask savedTask = GetSavedTask();

            Assert.That(savedTask.taskID, Is.EqualTo(TestConstants.TEST_TASK_ID));
            Assert.That(savedTask.taskName, Is.EqualTo(TestConstants.TEST_TASK_NAME));
            Assert.That(savedTask.listName, Is.EqualTo(TestConstants.TEST_TASK_LIST));
            Assert.That(savedTask.dueDate, Is.EqualTo(TestConstants.GetTestDateType()));
            Assert.That(savedTask.isImportant, Is.EqualTo(TestConstants.TEST_IS_IMPORTANT));
            Assert.That(savedTask.isDone, Is.EqualTo(TestConstants.TEST_IS_DONE));
        }

        private TodoTask GetSavedTask()
        {
            string savedTasks = TaskManager.GetInstance().GetTasks();
            TodoTask[]? tasks = JsonConvert.DeserializeObject<TodoTask[]>(savedTasks);

            if (tasks == null) Assert.Fail("The deserialization failed.");
            if (tasks!.Length < 1) Assert.Fail("There are no tasks saved.");

            TodoTask? savedTask = TaskManager.GetInstance().GetTaskById(TestConstants.TEST_TASK_ID);

            if (savedTask == null) Assert.Fail("The saved task ist null.");

            return savedTask!;
        }

    }
}