using Newtonsoft.Json;
using TodoServer;

namespace TodoServerNUnitTests
{
    public class TaskManagerTests
    {
        [SetUp]
        public void Setup()
        {
            // Setup before every Test
            
        }

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
        public void TaskManagerGetTasksToJSONTest()
        {
            SaveTestTask();

            string savedTasks = TaskManager.GetInstance().GetTasks();
            StringAssert.AreEqualIgnoringCase("[{\"taskID\":\"ID\",\"taskName\":\"Task\",\"taskList\":\"Todo\",\"dueDate\":null,\"isImportant\":false,\"isDone\":false}]", savedTasks);
        }

        [Test]
        public void TaskManagerGetTasksFromJSONTest()
        {
            SaveTestTask();

            string savedTasks = TaskManager.GetInstance().GetTasks();
            TodoTask[]? tasks = JsonConvert.DeserializeObject<TodoTask[]>(savedTasks);
            TodoTask newTask = tasks[0];

            Assert.That(newTask.taskID, Is.EqualTo("ID"));
            Assert.That(newTask.taskName, Is.EqualTo("Task"));
            Assert.That(newTask.taskList, Is.EqualTo("Todo"));
            Assert.That(newTask.dueDate, Is.EqualTo(null));
            Assert.That(newTask.isImportant, Is.EqualTo(false));
            Assert.That(newTask.isDone, Is.EqualTo(false));
        }

        private void SaveTestTask()
        {
            if (TaskManager.GetInstance().GetTaskById("ID") != null) return;

            TodoTask task = new TodoTask(
                "ID",
                "Task",
                "Todo",
                null,
                false,
                false
            );

            TaskManager.GetInstance().SaveTask(task);
        }
    }
}