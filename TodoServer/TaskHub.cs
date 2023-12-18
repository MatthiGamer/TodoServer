using Microsoft.AspNetCore.SignalR;

namespace TodoApplication
{
    public class TaskHub : Hub
    {
        public async Task SendInitialMessage(string message)
        {
            Console.WriteLine($"Received initial message: {message}");

            // Use the Context.ConnectionId to get the unique connection ID
            string connectionId = Context.ConnectionId;

            // Send a response back to the client (if needed)
            await Clients.Client(connectionId).SendAsync("ReceiveInitialResponse", "Hello from ASP.NET!");
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}