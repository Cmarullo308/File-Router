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
    public partial class ConfirmationWindow : Form {
        MainWindow mainWindow;

        public ConfirmationWindow(MainWindow mainWindow, string title, string question) {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.Text = title;
            this.questionText.Text = question;
        }

        private void yesButton_Click(object sender, EventArgs e) {
            mainWindow.confirmation = Confirmation.Yes;
            Close();
        }

        private void noButton_Click(object sender, EventArgs e) {
            mainWindow.confirmation = Confirmation.No;
            Close();
        }
    }
}
