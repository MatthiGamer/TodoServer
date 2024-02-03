using Newtonsoft.Json;

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

        public static string GetTasks()
        {
            return JsonConvert.SerializeObject(tasks);
        }

        public static TodoTask? GetTaskById(string id)
        {
            foreach (TodoTask task in tasks)
            {
                if (task.taskID == id) return task;
            }

            return null;
        }
    }
}
