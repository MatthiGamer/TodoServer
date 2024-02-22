using Newtonsoft.Json;

namespace TodoServer
{
    public class TaskManager
    {
        // Singleton instance
        private static TaskManager? instance = null;
        private static List<TodoTask> tasks = new List<TodoTask>();

        // Hide constructor
        private TaskManager() { }

        public static TaskManager GetInstance()
        {
            if (instance == null)
            {
                instance = new TaskManager();
            }

            return instance;
        }

        public void SaveTask(TodoTask task)
        {
            tasks.Add(task);
        }

        public void DeleteTask(string taskID)
        {
            TodoTask? task = GetTaskById(taskID);

            if (task == null)
            {
                Console.WriteLine($"TaskManagerWarning: Trying to delete task with id \"{taskID}\" but task couldn't be found.");
                return;
            }

            tasks.Remove(task);
        }

        /// <summary>
        /// Method for getting all saved tasks as JSON string
        /// </summary>
        /// <returns>Returns all saved tasks as JSON string</returns>
        public string GetTasks() => JsonConvert.SerializeObject(tasks);

        public TodoTask? GetTaskById(string taskID)
        {
            foreach (TodoTask task in tasks)
            {
                if (task.taskID == taskID) return task;
            }

            return null;
        }
    }
}
