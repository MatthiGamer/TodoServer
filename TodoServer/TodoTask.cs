namespace TodoServer
{
    [Serializable]
    public class TodoTask
    {
        public string taskID { get; }
        public string taskName { get; set; }
        public string taskList { get; set; }
        public TodoDate? dueDate { get; set; }
        public bool isImportant { get; set; }
        public bool isDone { get; set; }

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
