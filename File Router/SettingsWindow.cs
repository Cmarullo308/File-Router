using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Router {
    public partial class SettingsWindow : Form {
        MainWindow mainWindow;

        public SettingsWindow(MainWindow mainWindow) {
            InitializeComponent();

            this.mainWindow = mainWindow;

            //Set checkbox values
            startWithWindowsCheckBox.Checked = Program.settings.startWithWindows;
            startMinimizedCheckBox.Checked = Program.settings.startMinimized;
            showNotificationOnMinimizeCheckBox.Checked = Program.settings.showNotificationOnMinimize;
        }

        private void showNotificationOnMinimizeCheckBox_CheckedChanged(object sender, EventArgs e) {
            Program.settings.showNotificationOnMinimize = showNotificationOnMinimizeCheckBox.Checked;
            Program.saveSettings();
        }

        private void startWithWindowsCheckBox_CheckedChanged(object sender, EventArgs e) {
            Program.settings.startWithWindows = startWithWindowsCheckBox.Checked;
            Program.saveSettings();
            Program.setStartup();
        }

        private void startMinimizedCheckBox_CheckedChanged(object sender, EventArgs e) {
            Program.settings.startMinimized = startMinimizedCheckBox.Checked;
            Program.saveSettings();
        }

        private void SettingsWindow_Load(object sender, EventArgs e) {

        }
    }
}
