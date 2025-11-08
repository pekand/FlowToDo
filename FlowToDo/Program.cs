namespace FlowToDo
{
    internal static class Program
    {
        public static string appDataPath = "";
        public static string appDir = "";
        public static string backupDir = "";
        public static string defaultFlowTodoFile = "";

        public static Mutex? mutex = null;        
        public static string appName = "FlowToDo";
        public static string mainConfigFile = "config.FlowToDo";
        public static string defaultExtension = ".FlowToDo";

        [STAThread]
        static void Main(string[] args)
        {
            string pathToFlowToDoFile = args.Count() > 0 ? args[0] : "";

            // CREATE STANDARD PATHS
            appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDir = Path.Combine(appDataPath, Program.appName);
            Directory.CreateDirectory(appDir);
            backupDir = Path.Combine(appDir, "Backup");
            Directory.CreateDirectory(backupDir);
            defaultFlowTodoFile = Path.Combine(appDir, Program.mainConfigFile);

            ApplicationConfiguration.Initialize();
            Application.Run(new FormFlowToDo(pathToFlowToDoFile));

            if (mutex != null) { 
                mutex.ReleaseMutex();
                mutex = null;
            }
        }
    }
}