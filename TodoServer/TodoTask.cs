namespace TodoServer
{
    public class TodoTask
    {
        private string name;
        private string listName;
        private DateTime? dueDate;

        public TodoTask(string taskName, string listName, string? dueDate)
        {
            this.name = taskName;
            this.listName = listName;

            // Parse Date
            bool isSuccess = DateTime.TryParse(dueDate, out DateTime temp);
            if (isSuccess)
            {
                this.dueDate = temp;
                return;
            }

            this.dueDate = null;
            Console.WriteLine("DateConversionError", this);
        }
    }
}
