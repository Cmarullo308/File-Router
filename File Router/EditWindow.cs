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
    public partial class EditWindow : Form {
        MainWindow mainWindow;
        int indexNumber;
        Route route;

        string originalName;
        string originalSourceFolder;

        public EditWindow(MainWindow mainWindow, int indexNumber) {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.indexNumber = indexNumber;

            route = mainWindow.GetRouteData().routes.ElementAt(indexNumber); //Gets the route to be edited
            routeNameTextBox.Text = route.name;
            sourceDirectoryTextBox.Text = route.sourceFolder;
            destinationDirectoryTextBox.Text = route.destinationFolder;

            originalName = route.name;
            originalSourceFolder = route.sourceFolder;
        }

        private void applyButton_Click(object sender, EventArgs e) {
            //If the route name is already in use
            if (mainWindow.GetRouteData().routeNameInUse(routeNameTextBox.Text, indexNumber)) {
                createErrorWindow("Name in use", "The route name \"" + routeNameTextBox.Text + "\" is already in use");
                return;

            }
            //If the source directory is already in use by another route
            else if (mainWindow.GetRouteData().sourceDirectoryInUse(sourceDirectoryTextBox.Text, indexNumber)) {
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
            if (sourceDirectoryTextBox.Text == destinationDirectoryTextBox.Text) {
                createErrorWindow("Invalid Path", "The source directory and destination directory cannot be the same");
                return;
            }


            Route route = mainWindow.GetRouteData().routes.ElementAt(indexNumber);
            route.name = routeNameTextBox.Text;
            route.sourceFolder = sourceDirectoryTextBox.Text;
            route.destinationFolder = destinationDirectoryTextBox.Text;

            mainWindow.updateRouteNameList();
            mainWindow.GetRouteData().saveRoutesToFile();

            ListBox routeNameList = mainWindow.getRouteNameList();

            //Selects the newly added route in the list
            for (int i = 0; i < routeNameList.Items.Count; i++) {
                if (routeNameList.Items[i].ToString() == routeNameTextBox.Text) {
                    routeNameList.SelectedIndex = i;
                }
            }

            this.Close();
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
        /// Creates error window
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void createErrorWindow(string title, string message) {
            ErrorWindow errorWindow = new ErrorWindow(mainWindow, title, message);
            errorWindow.StartPosition = FormStartPosition.CenterParent;
            errorWindow.ShowDialog(this);
        }

        /// <summary>
        /// Closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        /// <summary>
        /// Opens a directory chooser to choose a source directory
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
        /// Opens a directory chooser to choose a destination directory
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
