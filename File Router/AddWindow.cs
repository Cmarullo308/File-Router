using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace File_Router {
    public partial class AddWindow : Form {
        public MainWindow mainWindow;

        public AddWindow() {
            InitializeComponent();
        }

        private void addbutton_Click(object sender, EventArgs e) {
            //Checks if the route name entered is already in use
            if (mainWindow.GetRouteData().routeNameInUse(routeNameTextBox.Text)) {
                createErrorWindow("Name in use", "The route name \"" + routeNameTextBox.Text + "\" is already in use");
                return;
            }
            //Checks if the source directory entered is already in use
            else if (mainWindow.GetRouteData().sourceDirectoryInUse(sourceDirectoryTextBox.Text)) {
                createErrorWindow("Source directory in use", "That source directory is already in use by another route");
                return;
            }

            //Check if the source directory entered is valid
            if (!validPath("source")) {
                createErrorWindow("Invalid Path", "\"" + sourceDirectoryTextBox.Text + "\" is an invalid source path");
                return;
            }

            //Check if the destination directory entered is valid
            if (!validPath("destination")) {
                createErrorWindow("Invalid Path", "\"" + destinationDirectoryTextBox.Text + "\" is an invalid destination path");
                return;
            }

            //Checks that the source directory is not the same as the destination directory
            if(sourceDirectoryTextBox.Text == destinationDirectoryTextBox.Text) {
                createErrorWindow("Invalid Path", "The source directory and destination directory cannot be the same");
                return;
            }

            //Adds new route
            mainWindow.GetRouteData().AddRoute(routeNameTextBox.Text, sourceDirectoryTextBox.Text, destinationDirectoryTextBox.Text);

            ////Selects the newly added route in the list
            ListBox routeNameList = mainWindow.getRouteNameList();
            for (int i = 0; i < routeNameList.Items.Count; i++) {
                if (routeNameList.Items[i].ToString() == routeNameTextBox.Text) {
                    routeNameList.SelectedIndex = i;
                }
            }

            Close();
        }

        /// <summary>
        ///     Determines if the path is valid
        /// </summary>
        /// <param name="pathField"></param>
        /// <returns>True if the path is valid, false if the path is invalid</returns>
        private bool validPath(string pathField) {
            string path;
            if (pathField == "source") {
                path = sourceDirectoryTextBox.Text;
            }
            else {
                path = destinationDirectoryTextBox.Text;
            }

            if (Path.IsPathRooted(path)) {
                try {
                    Path.GetFullPath(sourceDirectoryTextBox.Text);
                }
                catch (Exception) {
                    return false;
                }
            }
            else {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates an error window with a specific title and message
        /// </summary>
        /// <param name="title">Title of the error window</param>
        /// <param name="message">Message in the error window</param>
        public void createErrorWindow(string title, string message) {
            ErrorWindow errorWindow = new ErrorWindow(mainWindow, title, message);
            errorWindow.StartPosition = FormStartPosition.CenterParent;
            errorWindow.ShowDialog(this);
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Source directory choose button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseSourceDirectoryButton_Click(object sender, EventArgs e) {
            String directory = "C:\\";

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = directory;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                directory = dialog.FileName;
            }

            sourceDirectoryTextBox.Text = directory;
        }

        /// <summary>
        /// Destination directory choose button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseDestinationDirectoryButton_Click(object sender, EventArgs e) {
            String directory = "C:\\";

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = directory;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                directory = dialog.FileName;
            }

            destinationDirectoryTextBox.Text = directory;
        }
    }
}
