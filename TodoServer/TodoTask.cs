namespace TodoServer
{
    [Serializable]
    public class TodoTask
    {
        private string taskID;
        private string taskName;
        private string listName;
        private TodoDate? dueDate;
        private bool isImportant;
        private bool isDone;

        public TodoTask(string taskID, string taskName, string listName, TodoDate? dueDate, bool isImportant, bool isDone)
        {
            this.taskID = taskID;
            this.taskName = taskName;
            this.listName = listName;
            this.dueDate = dueDate;
            this.isImportant = isImportant;
            this.isDone = isDone;
        }
    }
}
