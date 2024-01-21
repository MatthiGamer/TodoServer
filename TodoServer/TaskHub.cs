﻿using Microsoft.AspNetCore.SignalR;
using TodoServer;

namespace TodoApplication
{
    public class TaskHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task ReceiveTask(string id, string taskName, string listName, string? dueDateString, bool isImportant, bool isDone)
        {
            Console.WriteLine("Task received");

            if (taskName == null || listName == null) return;

            TodoTask task = new TodoTask(id, taskName, listName, dueDateString, isImportant, isDone);

            Console.WriteLine("Task saved.");
            await SendMessage(Context.User!.ToString()!, "Task saved.");
        }
    }
}