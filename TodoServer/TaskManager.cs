using Newtonsoft.Json;

namespace TodoServer
{
    public class TaskManager
    {
        // Singleton instance
        private static TaskManager? instance = null;
        private List<TodoTask> tasks = new List<TodoTask>();

        // Hide constructor
        private TaskManager() { }

        public static TaskManager GetInstance()
        {
            // Server started
            if (instance == null)
            {
                instance = new TaskManager();
                // instance.LoadTasksFromDB();
            }

            return instance;
        }

        public void SaveTask(TodoTask task)
        {
            tasks.Add(task);
            // TODO: await DatabaseManager.SaveTask(task);
        }

        public void DeleteTask(string taskID)
        {
            TodoTask? task = GetTaskById(taskID);

            if (task == null)
            {
                Logging.LogWarning($"Trying to delete task with id \"{taskID}\" but task couldn't be found.", "TaskManagerWarning");
                return;
            }

            tasks.Remove(task);
            // TODO: await DatabaseManager.DeleteTaskByIdFromDB(taskID);
        }

        /// <summary>
        /// Method for getting all saved tasks as JSON string
        /// </summary>
        /// <returns>Returns all saved tasks as JSON string</returns>
        public string GetTasks() => JsonConvert.SerializeObject(tasks);

        public TodoTask? GetTaskById(string taskID)
        {
            // TODO: Rework this => Add dictionary (Dictionary<string, TodoTask>) for TaskManager
            // TODO: task = await DatabaseManager.GetTaskByIdFromDB();
            foreach (TodoTask task in tasks)
            {
                if (task.taskID == taskID) return task;
            }

            return null;
        }

        private async void LoadTasksFromDB()
        {
            this.tasks = await DatabaseManager.GetTasksFromDB();
        }
    }
}
