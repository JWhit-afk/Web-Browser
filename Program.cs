using Web_Browser_CW1.Presenter;

namespace Web_Browser_CW1
{

    // Application constants.
    public static class AppConstants {

        private static readonly string ExecucationDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string DataFilePath = Path.Combine(ExecucationDirectory, "data");
        public static readonly string TestDataFilePath = Path.Combine(DataFilePath, "test_data");
    }

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Ensure data directory exists.
            Directory.CreateDirectory(AppConstants.DataFilePath);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Create the main application form and initialize the application.
            var app = new WebBrowser();
            AppBootstrapper.Initialise(app);

            // Run the application.
            Application.Run(app);
        }
    }
}