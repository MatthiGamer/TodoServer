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
    }
}