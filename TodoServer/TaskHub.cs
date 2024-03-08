using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using TodoServer;

namespace TodoApplication
{
    public class TaskHub : Hub
    {
        /// <summary>
        /// Send a message to a specific user
        /// </summary>
        /// <param name="user">User / Client the message should be sent to</param>
        /// <param name="message">Message that should be send</param>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// Remote method for saving a task
        /// </summary>
        /// <param name="taskID">TaskID => is generated automatically by client</param>
        /// <param name="taskName">Name of the task</param>
        /// <param name="listName">Name of the list that the task is part of</param>
        /// <param name="dueDateString">Date that the task is due to in the form of DateType as JSON string</param>
        public async Task SaveTask(string taskID, string taskName, string listName, string dueDateString, bool isImportant, bool isDone)
        {
            // TODO: Send Task as JSON string and deserialize it here

            Logging.Log("New Task received.");

            if (taskName == null || listName == null) return;

            TodoTask? newTask = TaskManager.GetInstance().GetTaskById(taskID);
            if (newTask != null)
            {
                Logging.Log("Task already exists.");
                return;
            }

            DateType? dueDate = JsonConvert.DeserializeObject<DateType?>(dueDateString);
            newTask = new TodoTask(taskID, taskName, listName, dueDate, isImportant, isDone);
            TaskManager.GetInstance().SaveTask(newTask);

            // TODO: Log saving
            Logging.Log($"Task saved. => {newTask}");
            // TODO: Call notification method on client (not implemented yet)

            await Clients.All.SendAsync("AddTask", JsonConvert.SerializeObject(newTask));
        }

        /// <returns>Returns all saved tasks as JSON string</returns>
        public string GetTasks()
        {
            return TaskManager.GetInstance().GetTasks();
        }

        /// <summary>
        /// Remote method for updating the importance status of a task
        /// </summary>
        public async Task SaveTaskImportance(string taskID, bool isImportant)
        {
            TodoTask? task = TaskManager.GetInstance().GetTaskById(taskID);

            if (task == null)
            {
                Logging.LogWarning("Couldn't find task by id. Importance won't be changed.", "ServerWarning");
                return;
            }

            task.isImportant = isImportant;

            // TODO: Log change in importance
            // TODO: Call notification method on client (not implemented yet)

            await Clients.All.SendAsync("ChangeTaskImportance", task.taskID, isImportant);
        }

        /// <summary>
        /// Remote method for updating the done status of a task
        /// </summary>
        public async Task SaveTaskDone(string taskID, bool isDone)
        {
            TodoTask? task = TaskManager.GetInstance().GetTaskById(taskID);

            if (task == null)
            {
                Logging.LogWarning("Couldn't find task by id. Done status won't be changed.", "ServerWarning");
                return;
            }

            task.isDone = isDone;

            // TODO: Log change in done status
            // TODO: Call notification method on client (not implemented yet)

            await Clients.All.SendAsync("ChangeTaskDone", task.taskID, isDone);
        }

        /// <summary>
        /// Remote method for deleting a task
        /// </summary>
        public async Task DeleteTask(string taskID)
        {
            TodoTask? task = TaskManager.GetInstance().GetTaskById(taskID);

            if (task == null)
            {
                Logging.LogWarning("Couldn't find task by id. Task won't be deleted.", "ServerWarning");
                return;
            }

            TaskManager.GetInstance().DeleteTaskById(task.taskID);

            // TODO: Log deletion
            // TODO: Call notification method on client (not implemented yet)

            await Clients.All.SendAsync("DeleteTask", task.taskID);
        }
    }
}