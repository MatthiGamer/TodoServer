namespace TodoServer
{
    [Serializable]
    public class TodoTask
    {
        public string taskID { get; }
        public string taskName { get; }
        public string taskList { get; }
        public TodoDate? dueDate { get; }
        public bool isImportant { get; }
        public bool isDone { get; }

        public TodoTask(string taskID, string taskName, string listName, TodoDate? dueDate, bool isImportant, bool isDone)
        {
            this.taskID = taskID;
            this.taskName = taskName;
            this.taskList = listName;
            this.dueDate = dueDate;
            this.isImportant = isImportant;
            this.isDone = isDone;
        }
    }
}
