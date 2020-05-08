using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace File_Router {
    public partial class MainWindow : Form {
        private readonly RouteData routeData;

        private System.Threading.Timer routeTimer;
        bool setupComplete;
        bool isTransfering;

        string selectedRouteName;
        public Confirmation confirmation;

        //private bool allowshowdisplay = false;

        public MainWindow() {
            setupComplete = false;
            InitializeComponent();

            numberOfMinutesTextBox.Enabled = Program.settings.timedTransfers;
            numberOfMinutesTextBox.Text = (Program.settings.timerTimeInMinutes) + "";
            timedTransferCheckBox.Checked = Program.settings.timedTransfers;


            routeData = new RouteData(this);
            routeData.loadRoutesFromFile();

            notifyIcon.ContextMenuStrip = notifyIconContextMenuStrip;

            isTransfering = false;
            confirmation = Confirmation.No;
            setupComplete = true;
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            if (Program.settings.startMinimized) {
                this.WindowState = FormWindowState.Minimized;
            }
            setupTimer();
        }

        /// <summary>
        /// Starts the transfer timer if enabled
        /// </summary>
        private void setupTimer() {
            if (Program.settings.timedTransfers) {
                routeTimer = new System.Threading.Timer(startRoutingFiles, null, Program.settings.timerTimeInMinutes * 60000, Program.settings.timerTimeInMinutes * 60000);
            }
            else {
                routeTimer = new System.Threading.Timer(startRoutingFiles, null, Timeout.Infinite, Timeout.Infinite);
            }
        }

        private void startRoutingFiles(object state) {
            startRouteFilesThread();
        }

        /// <summary>
        /// Starts the routeFiles method in a thread
        /// </summary>
        private void startRouteFilesThread() {
            if (isTransfering) {
                return;
            }

            Thread routeFilesThread = new Thread(() => routeFiles());
            routeFilesThread.Start();
        }

        /// <summary>
        /// Transfers all the files in the routes
        /// </summary>
        public void routeFiles() {
            Console.WriteLine("Start of routeFiles()" + DateTime.Now);
            isTransfering = true;

            this.InvokeEx(f => f.routeFilesLabel.Text = "Routing files....");

            DateTime moment = DateTime.Now;
            string transferLogPath = @"Logs\Transfer Logs\" + Program.getMonthFromInt(moment.Month) + " " + moment.Year + " Transfer Log.txt";
            string transferLogText = "-----------" + moment.ToString() + "-----------\n";

            //For each route
            foreach (Route route in routeData.routes) {
                int filesMoved = 0;

                string[] files = null;

                try {
                    files = Directory.GetFiles(route.sourceFolder);
                }
                catch (System.IO.DirectoryNotFoundException e) {
                    Thread errorWindowThread = new Thread(() => createErrorWindow("Source directory not found", "The source directory \"" + route.sourceFolder + "\" cannot be found"));
                    errorWindowThread.Start();
                    Program.addToErrorLog(e.ToString());
                    continue;
                }
                catch (System.IO.IOException e) {
                    Thread errorWindowThread = new Thread(() => createErrorWindow("Destination directory not found", "The destination directory \"" + route.destinationFolder + "\" cannot be found"));
                    errorWindowThread.Start();
                    Program.addToErrorLog(e.ToString());
                    continue;
                }

                //For each file in the source folder
                foreach (string filePath in files) {
                    //If a file with the same name already exists in the source folder
                    if (File.Exists(route.destinationFolder + "\\" + Path.GetFileName(filePath))) {
                        int num = 1;

                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string extention = Path.GetExtension(filePath);
                        string newPath = route.destinationFolder + "\\" + fileName + " (" + num + ")" + extention;
                        while (File.Exists(newPath)) {
                            num++;
                            newPath = route.destinationFolder + "\\" + fileName + " (" + num + ")" + extention;
                        }

                        try {
                            File.Move(filePath, route.destinationFolder + "\\" + Path.GetFileName(newPath));
                            filesMoved++;
                        }
                        catch (Exception e) {
                            Thread errorWindowThread = new Thread(() => createErrorWindow("Exception thrown", e.ToString()));
                            errorWindowThread.Start();
                            break;
                        }
                    }
                    else {
                        try {
                            File.Move(filePath, route.destinationFolder + "\\" + Path.GetFileName(filePath));
                            filesMoved++;
                        }
                        catch (System.IO.DirectoryNotFoundException) {
                            Thread errorWindowThread = new Thread(() => createErrorWindow("Exception thrown", "The directory \"" + route.destinationFolder + "\" does not exist or cannot be accessed"));
                            errorWindowThread.Start();
                            break;
                        }
                        catch (System.IO.IOException) {
                            Thread errorWindowThread = new Thread(() => createErrorWindow("Exception thrown", "The directory \"" + route.destinationFolder + "\" does not exist or cannot be accessed"));
                            errorWindowThread.Start();
                            break;
                        }
                    }
                }
                if (filesMoved > 0) {
                    transferLogText += "-" + filesMoved + " files moved for the route \"" + route.name + "\"\n";
                }
            }

            transferLogText += "------------------------------------------";
            writeToTransferLogFile(transferLogPath, transferLogText);

            isTransfering = false;
            this.InvokeEx(f => f.routeFilesLabel.Text = "Route Files");
        }

        private void QuitProgram() {
            System.Windows.Forms.Application.ExitThread();
        }

        /// <summary>
        /// Logs file transfers
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        public void writeToTransferLogFile(string filePath, string message) {
            if (!Directory.Exists(@"Logs\Transfer Logs")) {
                Directory.CreateDirectory(@"Logs\Transfer Logs");
            }

            if (File.Exists(filePath)) {
                File.AppendAllText(filePath, "\n\n" + message);
            }
            else {
                File.WriteAllText(filePath, message);
            }
        }

        /// <summary>
        /// Creates an error window
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void createErrorWindow(string title, string message) {
            ErrorWindow errorWindow = new ErrorWindow(this, title, message);
            errorWindow.StartPosition = FormStartPosition.CenterParent;
            errorWindow.ShowDialog();
        }

        public RouteData GetRouteData() {
            return routeData;
        }

        public ListBox getRouteNameList() {
            return routeNameList;
        }

        /// <summary>
        /// Updates and sorts the items in the route name list
        /// </summary>
        public void updateRouteNameList() {
            routeData.routes.Sort();
            routeNameList.Items.Clear();

            foreach (Route route in routeData.routes) {
                routeNameList.Items.Add(route.name);
            }
        }

        /// <summary>
        /// Updates labels when a route is selected from the route name list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routeNameList_SelectedIndexChanged(object sender, EventArgs e) {
            int index = routeNameList.SelectedIndex;
            if (index < 0) {
                return;
            }

            Route route = routeData.routes.ElementAt(index);

            nameLabel.Text = "Name: " + route.name;
            sourceLabel.Text = "Source: " + route.sourceFolder;
            destinationLabel.Text = "Destination: " + route.destinationFolder;

            nameLabel.Visible = true;
            sourceLabel.Visible = true;
            destinationLabel.Visible = true;
        }

        private void addButtonClicked(object sender, EventArgs e) {
            openAddWindow();
        }


        /// <summary>
        /// Opens the add route window
        /// </summary>
        private void openAddWindow() {
            if (isTransfering) {
                ErrorWindow errorWindow = new ErrorWindow(this, "transfer in progress", "Cannot add or edit routes while a transfer is in progress");
                errorWindow.ShowDialog();
                return;
            }

            AddWindow addWindow = new AddWindow();
            addWindow.mainWindow = this;
            addWindow.ShowDialog(this);
        }

        /// <summary>
        /// Enables timed transfers in settings and sets up the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timedTransfer_CheckedChanged(object sender, EventArgs e) {
            Program.settings.timedTransfers = numberOfMinutesTextBox.Enabled = timedTransferCheckBox.Checked;
            Program.saveSettings();

            if (setupComplete) {
                if (timedTransferCheckBox.Checked) {
                    routeTimer.Change(Program.settings.timerTimeInMinutes * 60000, Program.settings.timerTimeInMinutes * 60000);
                }
                else {
                    routeTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }
            }
        }

        /// <summary>
        /// Hides taskbar icon when minimized, shows a notification is enabled and shows an icon in the tray
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Resize(object sender, EventArgs e) {
            bool MousePointerNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized && MousePointerNotOnTaskBar) {
                minimizeMessage();
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        /// <summary>
        /// Sends a notification saying "File Router Minimized to the tray"
        /// </summary>
        private void minimizeMessage() {
            if (Program.settings.showNotificationOnMinimize) {
                notifyIcon.BalloonTipText = "File Router Minimized to the tray";
                notifyIcon.ShowBalloonTip(1000);
            }
        }

        /// <summary>
        /// Opens the Add route menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMenuItem_Click(object sender, EventArgs e) {
            AddWindow addWindow = new AddWindow();
            addWindow.mainWindow = this;
            addWindow.ShowDialog(this);
        }

        /// <summary>
        /// Reopens program window when the notify icon is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal) {
                this.ShowInTaskbar = true;
                notifyIcon.Visible = false;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            deleteItem();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) {
            openAddWindow();
        }

        private void editButton_Click(object sender, EventArgs e) {
            editItem();
        }

        /// <summary>
        /// Opens the edit window for the selected route in the route name list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routeNameList_DoubleClick(object sender, EventArgs e) {
            editItem();
        }

        private void editMenuItem_Click(object sender, EventArgs e) {
            editItem();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e) {
            deleteItem();
        }

        /// <summary>
        /// Deletes the selected route from the route name list
        /// </summary>
        private void deleteItem() {
            int selectedRouteIndex = routeNameList.SelectedIndex;
            if (selectedRouteIndex < 0) {
                return;
            }

            ConfirmationWindow confirmationWindow = new ConfirmationWindow(this, "Are you sure?", "Are you sure you want to delete this route?");
            confirmationWindow.ShowDialog(this);

            if (confirmation == Confirmation.No) {
                return;
            }

            routeData.routes.RemoveAt(selectedRouteIndex);
            updateRouteNameList();

            if (routeNameList.Items.Count > 0) { //There are items in the list
                if (selectedRouteIndex >= routeNameList.Items.Count) {
                    routeNameList.SelectedIndex = selectedRouteIndex - 1;
                }
                else {
                    routeNameList.SelectedIndex = selectedRouteIndex;
                }
            }

            //Hide labels if theres no items in the route list
            if (routeNameList.Items.Count == 0) {
                hideLabels();
            }

            routeData.saveRoutesToFile();
        }

        /// <summary>
        /// Opens the edit window for the selected route
        /// </summary>
        public void editItem() {
            if (isTransfering) {
                ErrorWindow errorWindow = new ErrorWindow(this, "transfer in progress", "Cannot add or edit routes while a transfer is in progress");
                errorWindow.ShowDialog();
                return;
            }

            int index = routeNameList.SelectedIndex;
            if (index < 0) {
                return;
            }

            EditWindow editWindow = new EditWindow(this, index);
            editWindow.StartPosition = FormStartPosition.CenterParent;
            editWindow.ShowDialog(this);
        }

        /// <summary>
        /// Hides route labels if clicked on nothing. Shows right click context menu if right clicked on a route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routeNameList_MouseDown(object sender, MouseEventArgs e) {
            Point pt = new Point(e.X, e.Y);
            int index = routeNameList.IndexFromPoint(pt);

            if (index < 0) {
                routeNameList.SelectedItems.Clear();
                hideLabels();
                return;
            }

            if (e.Button == MouseButtons.Right) {
                if (index != ListBox.NoMatches) {
                    selectedRouteName = routeNameList.Items[index].ToString();
                    routeNameList.SelectedIndex = index;
                    routeContextMenuStrip.Show(Cursor.Position);
                    routeContextMenuStrip.Visible = true;
                }
            }
        }

        /// <summary>
        /// Hides the labels to the right of the route name list
        /// </summary>
        private void hideLabels() {
            nameLabel.Visible = false;
            sourceLabel.Visible = false;
            destinationLabel.Visible = false;
        }

        /// <summary>
        /// Starts transfering the files when the big route button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routeButtonClicked(object sender, EventArgs e) {
            startRouteFilesThread();
        }

        private void settingsMenuItem_Click(object sender, EventArgs e) {
            openSettingsWindow();
        }

        private void openSettingsWindow() {
            SettingsWindow settingsWindow = new SettingsWindow(this);
            settingsWindow.ShowDialog(this);
        }

        //Only allow integers in the Number of minutes text box
        private void numberOfMinutesTextBox_TextChanged(object sender, EventArgs e) {
            if (numberOfMinutesTextBox.Text.Contains(".")) {
                numberOfMinutesTextBox.Text = numberOfMinutesTextBox.Text.Replace(".", "");
            }

            if (numberOfMinutesTextBox.Text == "") {
                return;
            }

            if (numberOfMinutesTextBox.Text == "0") {
                numberOfMinutesTextBox.Text = "1";
            }


            Program.settings.timerTimeInMinutes = int.Parse(numberOfMinutesTextBox.Text);
            numberOfMinutesTextBox.Text = Program.settings.timerTimeInMinutes + "";
            Program.saveSettings();

            if (setupComplete) {
                routeTimer.Change(Program.settings.timerTimeInMinutes * 60000, Program.settings.timerTimeInMinutes * 60000);
            }
        }

        private void numberOfMinutesTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Sets minutes to 1 if left empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberOfMinutesTextBox_Leave(object sender, EventArgs e) {
            if (numberOfMinutesTextBox.Text == "") {
                numberOfMinutesTextBox.Text = "1";

                Program.settings.timerTimeInMinutes = int.Parse(numberOfMinutesTextBox.Text);
                numberOfMinutesTextBox.Text = Program.settings.timerTimeInMinutes + "";
                Program.saveSettings();

                if (setupComplete) {
                    routeTimer.Change(Program.settings.timerTimeInMinutes * 60000, Program.settings.timerTimeInMinutes * 60000);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) {
            editItem();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            deleteItem();
        }

        /// <summary>
        /// Minimize to tray when X button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            minimizeMessage();
        }

        private void exitMenuItem_Click(object sender, EventArgs e) {
            if (isTransfering) {
                ErrorWindow errorWindow = new ErrorWindow(this, "Transfer in progress", "There is a file transfer in progress");
                errorWindow.StartPosition = FormStartPosition.CenterScreen;
                errorWindow.ShowDialog();
                return;
            }

            QuitProgram();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (isTransfering) {
                ErrorWindow errorWindow = new ErrorWindow(this, "Transfer in progress", "There is a file transfer in progress");
                errorWindow.StartPosition = FormStartPosition.CenterScreen;
                errorWindow.ShowDialog();
                return;
            }
            QuitProgram();
        }

        private void routeFilesNowToolStripMenuItem_Click(object sender, EventArgs e) {
            startRouteFilesThread();
        }

        /// <summary>
        /// Reopens the main window if minimized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal) {
                this.ShowInTaskbar = true;
                notifyIcon.Visible = false;
            }
        }

        /// <summary>
        /// Keyboard shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e) {

            switch (e.KeyCode) {
                case Keys.Enter:
                    editItem();
                    return;
                case Keys.Delete:
                    deleteItem();
                    return;
                default:
                    break;
            }
        }

        /// <summary>
        /// Makes notifyIconContextMenu close when clicking outside it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIconContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked) {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Opens transfer log folder, creates it if it doesn't exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void transferLogsMenuItem_Click(object sender, EventArgs e) {
            if (!Directory.Exists(Program.transferLogsPath)) {
                Directory.CreateDirectory(Program.transferLogsPath);
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                FileName = Program.transferLogsPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        /// <summary>
        /// Opens error log folder, creates it if it doesn't exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void errorMenuItem_Click(object sender, EventArgs e) {
            if (!Directory.Exists(Program.errorLogsPath)) {
                Directory.CreateDirectory(Program.errorLogsPath);
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                FileName = Program.errorLogsPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        /// <summary>
        /// Opens info window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }

    public enum Confirmation {
        Yes,
        No
    }

    /// <summary>
    /// Access main thread from another thread
    /// </summary>
    public static class ISynchronizeInvokeExtensions {
        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke {
            if (@this.InvokeRequired) {
                @this.Invoke(action, new object[] { @this });
            }
            else {
                action(@this);
            }
        }
    }
}

