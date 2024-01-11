namespace TodoServer
{
    public class TaskManager
    {
        private static TaskManager? instance = null;

        public static TaskManager Instance ()
        {
            if (instance == null)
            {
                instance = new TaskManager ();
            }

            return instance;
        }
    }
}
