namespace Web_Browser_CW1
{

    // Application constants.
    public static class AppConstants {
        public const string DataFilePath = "C:/Users/Jacob/Desktop/data";
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
            Application.Run(new WebBrowser());
        }
    }
}