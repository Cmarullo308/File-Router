namespace File_Router {
    partial class AddWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWindow));
            this.nameLabel = new System.Windows.Forms.Label();
            this.routeNameTextBox = new System.Windows.Forms.TextBox();
            this.chooseSourceDirectoryButton = new System.Windows.Forms.Button();
            this.sourceDirectoryLabel = new System.Windows.Forms.Label();
            this.destinationDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseDestinationDirectoryButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sourceDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(115, 25);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "RouteName";
            // 
            // routeNameTextBox
            // 
            this.routeNameTextBox.Location = new System.Drawing.Point(201, 15);
            this.routeNameTextBox.Name = "routeNameTextBox";
            this.routeNameTextBox.Size = new System.Drawing.Size(492, 20);
            this.routeNameTextBox.TabIndex = 0;
            // 
            // chooseSourceDirectoryButton
            // 
            this.chooseSourceDirectoryButton.Location = new System.Drawing.Point(699, 51);
            this.chooseSourceDirectoryButton.Name = "chooseSourceDirectoryButton";
            this.chooseSourceDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.chooseSourceDirectoryButton.TabIndex = 2;
            this.chooseSourceDirectoryButton.Text = "Select";
            this.chooseSourceDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseSourceDirectoryButton.Click += new System.EventHandler(this.chooseSourceDirectoryButton_Click);
            // 
            // sourceDirectoryLabel
            // 
            this.sourceDirectoryLabel.AutoSize = true;
            this.sourceDirectoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceDirectoryLabel.Location = new System.Drawing.Point(12, 49);
            this.sourceDirectoryLabel.Name = "sourceDirectoryLabel";
            this.sourceDirectoryLabel.Size = new System.Drawing.Size(157, 25);
            this.sourceDirectoryLabel.TabIndex = 3;
            this.sourceDirectoryLabel.Text = "Source Directory";
            // 
            // destinationDirectoryTextBox
            // 
            this.destinationDirectoryTextBox.Location = new System.Drawing.Point(201, 93);
            this.destinationDirectoryTextBox.Name = "destinationDirectoryTextBox";
            this.destinationDirectoryTextBox.Size = new System.Drawing.Size(492, 20);
            this.destinationDirectoryTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Destination Directory";
            // 
            // chooseDestinationDirectoryButton
            // 
            this.chooseDestinationDirectoryButton.Location = new System.Drawing.Point(699, 91);
            this.chooseDestinationDirectoryButton.Name = "chooseDestinationDirectoryButton";
            this.chooseDestinationDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.chooseDestinationDirectoryButton.TabIndex = 4;
            this.chooseDestinationDirectoryButton.Text = "Select";
            this.chooseDestinationDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseDestinationDirectoryButton.Click += new System.EventHandler(this.chooseDestinationDirectoryButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(3, 10);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addbutton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(141, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Location = new System.Drawing.Point(284, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 43);
            this.panel1.TabIndex = 5;
            // 
            // sourceDirectoryTextBox
            // 
            this.sourceDirectoryTextBox.Location = new System.Drawing.Point(201, 54);
            this.sourceDirectoryTextBox.Name = "sourceDirectoryTextBox";
            this.sourceDirectoryTextBox.Size = new System.Drawing.Size(492, 20);
            this.sourceDirectoryTextBox.TabIndex = 1;
            // 
            // AddWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 182);
            this.Controls.Add(this.routeNameTextBox);
            this.Controls.Add(this.sourceDirectoryTextBox);
            this.Controls.Add(this.destinationDirectoryTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chooseDestinationDirectoryButton);
            this.Controls.Add(this.sourceDirectoryLabel);
            this.Controls.Add(this.chooseSourceDirectoryButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Route";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox routeNameTextBox;
        private System.Windows.Forms.Button chooseSourceDirectoryButton;
        private System.Windows.Forms.Label sourceDirectoryLabel;
        private System.Windows.Forms.TextBox destinationDirectoryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseDestinationDirectoryButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox sourceDirectoryTextBox;
    }
}