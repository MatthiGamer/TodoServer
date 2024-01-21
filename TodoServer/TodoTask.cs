namespace TodoServer
{
    public class TodoTask
    {
        private string id;
        private string taskName;
        private string listName;
        private DateTime? dueDate;
        private bool isImportant;
        private bool isDone;

        public TodoTask(string id, string taskName, string listName, string? dueDate, bool isImportant, bool isDone)
        {
            this.id = id;
            this.taskName = taskName;
            this.listName = listName;
            this.isImportant = isImportant;
            this.isDone = isDone;

            if (dueDate == null)
            {
                this.dueDate = null;
                return;
            }

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
