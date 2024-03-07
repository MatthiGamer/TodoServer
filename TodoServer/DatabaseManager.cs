using System.Data.Common;
using System.Data.SQLite;

namespace TodoServer
{
    public class DatabaseManager
    {
        private const string DATABASE_PATH = "TaskDB.db";
        private const string CONNECTION_STRING = $"Data Source={DATABASE_PATH}";

        private const string DATABASE_READER_ERROR_NAME = "DatabaseReaderError";

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
