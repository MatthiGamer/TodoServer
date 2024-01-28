namespace TodoServer
{
    public class TaskManager
    {
        private static TaskManager? instance = null;
        private static List<TodoTask> tasks = new List<TodoTask>();

        public static TaskManager Instance ()
        {
            if (instance == null)
            {
                instance = new TaskManager();
            }

            return instance;
        }

        public static void SaveTask(TodoTask task)
        {
            tasks.Add(task);
        }

        public static TodoTask[] GetTasks() => tasks.ToArray();
    }
}
