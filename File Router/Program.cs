using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace File_Router {
    static class Program {
        public static string version = "1.0";

        public static Settings settings;
        public static string transferLogsPath = @"Logs\Transfer Logs";
        public static string errorLogsPath = @"Logs\Error Logs";

        [STAThread]
        static void Main() {
            try {
                settings = new Settings();
                loadSettings();
                createLogsFolders();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            catch (Exception e) {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" + e.ToString() + "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                addToErrorLog(e.ToString());
            }
        }

        /// <summary>
        /// Checks if the log folders exist and creates them if they don't
        /// </summary>
        private static void createLogsFolders() {
            if (!Directory.Exists(transferLogsPath)) {
                Directory.CreateDirectory(transferLogsPath);
            }

            if (!Directory.Exists(errorLogsPath)) {
                Directory.CreateDirectory(errorLogsPath);
            }
        }

        /// <summary>
        /// Saves error to log
        /// </summary>
        /// <param name="error"></param>
        public static void addToErrorLog(string error) {

            string dir = @"Logs\Error Logs";

            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            DateTime moment = DateTime.Now;

            string fileName = dir + "\\" + getMonthFromInt(moment.Month) + " " + moment.Year + " Error Log.txt";

            //If the file exists
            if (File.Exists(fileName)) {
                File.AppendAllText(fileName, "\n\n--------Error at " + moment.ToString() + "--------\n" + error + "\n------------------------------------------------------------------------------------------------");
            }
            else { //Create new file
                File.WriteAllText(fileName, "--------Error at " + moment.ToString() + "--------\n" + error + "\n------------------------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Gets a month string from an int
        /// </summary>
        /// <param name="monthNumber"></param>
        /// <returns></returns>
        public static String getMonthFromInt(int monthNumber) {
            if (monthNumber < 1 || monthNumber > 12) {
                return "InvalidMonthNumber";
            }

            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            return months[monthNumber - 1];
        }

        /// <summary>
        /// Loads settings from file
        /// </summary>
        public static void loadSettings() {
            if (File.Exists(@"Data\Settings.json")) {
                string jsonResult = File.ReadAllText(@"Data\Settings.json");
                settings = JsonConvert.DeserializeObject<Settings>(jsonResult);
            }
        }

        /// <summary>
        /// Saves settings to file
        /// </summary>
        public static void saveSettings() {
            if (!Directory.Exists("Data")) {
                Directory.CreateDirectory("Data");
            }

            string jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(@"Data\Settings.json", jsonString);
        }

        /// <summary>
        /// Enables or disables starting at login
        /// </summary>
        public static void setStartup() {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (settings.startWithWindows) {
                reg.SetValue("File Router", "\"" + Application.ExecutablePath.ToString().Replace("/", "\\") + "\"");
            }
            else {
                reg.DeleteValue("File Router", false);
            }

        }
    }
}
