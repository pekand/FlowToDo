namespace FlowToDo
{
    internal static class Program
    {
        public static Mutex mutex = null;        
        public static string appName = "FlowToDo";
        public static string mainConfigFile = "config.FlowToDo";

        [STAThread]
        static void Main(string[] args)
        {
            string pathToFlowToDoFile = args.Count() > 0 ? args[0] : "";

            ApplicationConfiguration.Initialize();
            Application.Run(new FormFlowToDo(pathToFlowToDoFile));

            if (mutex != null) { 
                mutex.ReleaseMutex();
                mutex = null;
            }
        }
    }
}