using TodoServer;

namespace TodoServerNUnitTests
{
    public class TaskManagerTestsWithoutSetup
    {
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
        public void TaskManagerSaveTaskTest()
        {
            TodoTask task = new TodoTask(
                TestConstants.TEST_SAVE_TASK_ID,
                TestConstants.TEST_TASK_NAME,
                TestConstants.TEST_TASK_LIST,
                TestConstants.GetTestDateType(),
                TestConstants.TEST_IS_IMPORTANT,
                TestConstants.TEST_IS_DONE
            );

            Assert.DoesNotThrow(() => TaskManager.GetInstance().SaveTask(task));
            Assert.DoesNotThrow(() => TaskManager.GetInstance().DeleteTask(task.taskID));
        }
    }
}
