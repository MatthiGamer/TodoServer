using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using TodoServer;

namespace TodoApplication
{
    public class TaskHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SaveTask(string id, string taskName, string listName, string dueDateString, bool isImportant, bool isDone)
        {
            Console.WriteLine("New Task received.");

            if (taskName == null || listName == null) return;

            TodoTask? newTask = TaskManager.GetInstance().GetTaskById(id);
            if (newTask != null)
            {
                Console.WriteLine("Task already exists.");
                return;
            }

            DateType? dueDate = JsonConvert.DeserializeObject<DateType?>(dueDateString);
            newTask = new TodoTask(id, taskName, listName, dueDate, isImportant, isDone);
            TaskManager.GetInstance().SaveTask(newTask);

            const string TASK_SAVED = "Task successfully saved.";
            Console.WriteLine(TASK_SAVED);
            await SendMessage(Context.User!.ToString()!, TASK_SAVED);

            await Clients.All.SendAsync("AddTask", JsonConvert.SerializeObject(newTask));
        }

        public async Task<string> GetTasks()
        {
            return TaskManager.GetInstance().GetTasks();
        }

        public async Task SaveTaskImportance(string id, bool isImportant)
        {
            TodoTask? task = TaskManager.GetInstance().GetTaskById(id);

            if (task == null)
            {
                Console.WriteLine("ServerWarning: Couldn't find task by id. Importance won't be changed.");
                return;
            }

            task.isImportant = isImportant;

            const string TASK_IMPORTANCE_CHANGED = "Task importance changed.";
            Console.WriteLine(TASK_IMPORTANCE_CHANGED);
            await SendMessage(Context.User!.ToString()!, TASK_IMPORTANCE_CHANGED);

            await Clients.All.SendAsync("ChangeTaskImportance", task.taskID, isImportant);
        }

        public async Task SaveTaskDone(string id, bool isDone)
        {
            TodoTask? task = TaskManager.GetInstance().GetTaskById(id);

            if (task == null)
            {
                Console.WriteLine("ServerWarning: Couldn't find task by id. Done status won't be changed.");
                return;
            }

            task.isDone = isDone;

            const string TASK_DONE_STATUS_CHANGED = "Task done status changed.";
            Console.WriteLine(TASK_DONE_STATUS_CHANGED);
            await SendMessage(Context.User!.ToString()!, TASK_DONE_STATUS_CHANGED);

            await Clients.All.SendAsync("ChangeTaskDone", task.taskID, isDone);
        }
    }
}