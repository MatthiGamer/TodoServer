namespace TodoServer
{
    [Serializable]
    public class TodoTask
    {
        public string taskID { get; }
        public string taskName { get; set; }
        public string listName { get; set; }
        public DateType? dueDate { get; set; }
        public bool isImportant { get; set; }
        public bool isDone { get; set; }

        public TodoTask(string taskID, string taskName, string listName, DateType? dueDate, bool isImportant, bool isDone)
        {
            this.taskID = taskID;
            this.taskName = taskName;
            this.listName = listName;
            this.dueDate = dueDate;
            this.isImportant = isImportant;
            this.isDone = isDone;
        }

        public override string ToString()
        {
            return $"{{ID: {this.taskID}, Name: {this.taskName}, List: {this.listName}, " +
                   $"Due to: {(this.dueDate != null ? this.dueDate : "End of time")}, isImportant: {this.isImportant}, " +
                   $"isDone: {this.isDone}}}";
        }
    }
}
