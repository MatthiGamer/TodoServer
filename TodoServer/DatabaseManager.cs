using System.Data.Common;
using System.Data.SQLite;

namespace TodoServer
{
    public class DatabaseManager
    {
        private const string DATABASE_PATH = "TaskDB.db";
        private const string CONNECTION_STRING = $"Data Source={DATABASE_PATH}";

        private const string DATABASE_READER_ERROR_NAME = "DatabaseReaderError";

        /// <summary>
        /// Connects to the main database if no other connectionString is given and returns all tasks saved in it.
        /// </summary>
        /// <param name="connectionString">(Optional) Can be used to connect to a database other than TaskDB.db</param>
        /// <returns>Returns a list of all saved tasks.</returns>
        public static async Task<List<TodoTask>> GetTasksFromDB(string connectionString = CONNECTION_STRING)
        {
            List<TodoTask> tasks = new List<TodoTask>();

            TodoTask task;
            string taskID = string.Empty;
            string taskName = string.Empty;
            string listName = string.Empty;
            string? dueDateString = string.Empty;
            DateType? dueDate = null;
            bool isImportant = false;
            bool isDone = false;

            await using SQLiteConnection connection = new SQLiteConnection(connectionString);
            await connection.OpenAsync();
            await using SQLiteCommand command = new SQLiteCommand("SELECT * FROM Tasks", connection);
            try
            {
                await using DbDataReader dataReader = await command.ExecuteReaderAsync();
            

                while (await dataReader.ReadAsync())
                {
                    try
                    {
                        taskID = dataReader.GetString(0);
                        taskName = dataReader.GetString(1);
                        listName = dataReader.GetString(2);

                        dueDateString = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);

                        isImportant = IntToBool(dataReader.GetInt32(4));
                        isDone = IntToBool(dataReader.GetInt32(5));

                        dueDate = DateType.GetDateTypeFromString(dueDateString);

                        task = new TodoTask(taskID, taskName, listName, dueDate, isImportant, isDone);
                        tasks.Add(task);
                    }
                    catch (Exception exception)
                    {
                        Logging.LogError($"Error converting the values from the database.\nError: {exception.Message}", DATABASE_READER_ERROR_NAME);
                        continue;
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.LogError($"Error reading the database.\nError: {exception.Message}", DATABASE_READER_ERROR_NAME);
            }

            await connection.CloseAsync();
            return tasks;
        }

        /// <summary>
        /// Connects to the main database if no other connectionString is given and returns the task with <paramref name="taskID"/>.
        /// </summary>
        /// <param name="connectionString">(Optional) Can be used to connect to a database other than TaskDB.db</param>
        /// <returns>Returns the task with the specified <paramref name="taskID"/> if it is found, otherwise <see langword="null"/>.</returns>
        public static async Task<TodoTask?> GetTaskByIdFromDB(string taskID, string connectionString = CONNECTION_STRING)
        {
            TodoTask? task = null;

            await using SQLiteConnection connection = new SQLiteConnection(connectionString);
            await connection.OpenAsync();
            await using SQLiteCommand command = new SQLiteCommand($"SELECT * FROM Tasks WHERE ID = '{taskID}'", connection);

            try
            {
                await using DbDataReader dataReader = await command.ExecuteReaderAsync();
                bool hasFoundTask = await dataReader.ReadAsync();
                if (!hasFoundTask)
                {
                    Logging.LogWarning("Task not found.", "DatabaseWarning");
                    return null;
                }

                try
                {
                    string taskName = dataReader.GetString(1);
                    string listName = dataReader.GetString(2);

                    string? dueDateString = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);

                    bool isImportant = IntToBool(dataReader.GetInt32(4));
                    bool isDone = IntToBool(dataReader.GetInt32(5));

                    DateType? dueDate = DateType.GetDateTypeFromString(dueDateString);

                    task = new TodoTask(taskID, taskName, listName, dueDate, isImportant, isDone);
                }
                catch (Exception exception)
                {
                    Logging.LogError($"Error converting the values from the database.\nError: {exception.Message}", DATABASE_READER_ERROR_NAME);
                }
            }
            catch (Exception exception)
            {
                Logging.LogError($"Error reading the database.\nError: {exception.Message}", DATABASE_READER_ERROR_NAME);
            }

            await connection.CloseAsync();
            return task;
        }

        /// <summary>
        /// Connects to the main database if no other connectionString is given and saves the specified task in it.
        /// </summary>
        /// <param name="task">The task that should be saved</param>
        /// <param name="connectionString">(Optional) Can be used to connect to a database other than TaskDB.db</param>
        /// <returns>Returns an awaitable task.</returns>
        public static async Task SaveTask(TodoTask task, string connectionString = CONNECTION_STRING)
        {
            string valueString = $"'{task.taskID}', '{task.taskName}', '{task.listName}', {(task.dueDate != null ? $"'{task.dueDate}'" : "NULL")}, {BoolToInt(task.isImportant)}, {BoolToInt(task.isDone)}";
            string queryString = $"INSERT INTO Tasks (ID, Name, List, DueDate, IsImportant, IsDone) VALUES ({valueString})";

            await using SQLiteConnection connection = new SQLiteConnection(connectionString);
            await connection.OpenAsync();
            await using SQLiteCommand command = new SQLiteCommand(queryString, connection);

            try
            {
                int affectedRows = await command.ExecuteNonQueryAsync();
                if (affectedRows != 1) Logging.LogWarning($"Saving affected {affectedRows} rows instead of one.", "DatabaseWarning");
            }
            catch (Exception exception)
            {
                Logging.LogError($"Query couldn't be finished.\nError: {exception.Message}", "DatabaseError");
            }
            

            await connection.CloseAsync();
        }

        private static int BoolToInt(bool value) => value ? 1 : 0;
        private static bool IntToBool(int value) => value == 1;
    }
}
