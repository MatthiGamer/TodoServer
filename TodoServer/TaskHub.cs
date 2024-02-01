using Microsoft.AspNetCore.SignalR;
using TodoServer;

namespace TodoApplication
{
    public class TaskHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SaveTask(string id, string taskName, string listName, TodoDate? dueDate, bool isImportant, bool isDone)
        {
            Console.WriteLine("Task received");

            if (taskName == null || listName == null) return;

            TodoTask task = new TodoTask(id, taskName, listName, dueDate, isImportant, isDone);
            TaskManager.SaveTask(task);

            Console.WriteLine("Task saved.");
            await SendMessage(Context.User!.ToString()!, "Task saved.");
        }

        public async Task<string> GetTasks()
        {
            return TaskManager.GetTasks();
        }
    }
}