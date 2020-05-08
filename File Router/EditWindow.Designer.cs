namespace File_Router
{
    partial class EditWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditWindow));
            this.sourceDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.destinationDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseDestinationDirectoryButton = new System.Windows.Forms.Button();
            this.sourceDirectoryLabel = new System.Windows.Forms.Label();
            this.chooseSourceDirectoryButton = new System.Windows.Forms.Button();
            this.routeNameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourceDirectoryTextBox
            // 
            this.sourceDirectoryTextBox.Location = new System.Drawing.Point(201, 57);
            this.sourceDirectoryTextBox.Name = "sourceDirectoryTextBox";
            this.sourceDirectoryTextBox.Size = new System.Drawing.Size(492, 20);
            this.sourceDirectoryTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.applyButton);
            this.panel1.Location = new System.Drawing.Point(284, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 43);
            this.panel1.TabIndex = 5;
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
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(3, 10);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 0;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // destinationDirectoryTextBox
            // 
            this.destinationDirectoryTextBox.Location = new System.Drawing.Point(201, 96);
            this.destinationDirectoryTextBox.Name = "destinationDirectoryTextBox";
            this.destinationDirectoryTextBox.Size = new System.Drawing.Size(492, 20);
            this.destinationDirectoryTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 25);
            this.label1.TabIndex = 17;
            this.label1.Text = "Destination Directory";
            // 
            // chooseDestinationDirectoryButton
            // 
            this.chooseDestinationDirectoryButton.Location = new System.Drawing.Point(699, 94);
            this.chooseDestinationDirectoryButton.Name = "chooseDestinationDirectoryButton";
            this.chooseDestinationDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.chooseDestinationDirectoryButton.TabIndex = 4;
            this.chooseDestinationDirectoryButton.Text = "Select";
            this.chooseDestinationDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseDestinationDirectoryButton.Click += new System.EventHandler(this.chooseDestinationDirectoryButton_Click);
            // 
            // sourceDirectoryLabel
            // 
            this.sourceDirectoryLabel.AutoSize = true;
            this.sourceDirectoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceDirectoryLabel.Location = new System.Drawing.Point(12, 52);
            this.sourceDirectoryLabel.Name = "sourceDirectoryLabel";
            this.sourceDirectoryLabel.Size = new System.Drawing.Size(157, 25);
            this.sourceDirectoryLabel.TabIndex = 15;
            this.sourceDirectoryLabel.Text = "Source Directory";
            // 
            // chooseSourceDirectoryButton
            // 
            this.chooseSourceDirectoryButton.Location = new System.Drawing.Point(699, 54);
            this.chooseSourceDirectoryButton.Name = "chooseSourceDirectoryButton";
            this.chooseSourceDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.chooseSourceDirectoryButton.TabIndex = 2;
            this.chooseSourceDirectoryButton.Text = "Select";
            this.chooseSourceDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseSourceDirectoryButton.Click += new System.EventHandler(this.chooseSourceDirectoryButton_Click);
            // 
            // routeNameTextBox
            // 
            this.routeNameTextBox.Location = new System.Drawing.Point(201, 16);
            this.routeNameTextBox.Name = "routeNameTextBox";
            this.routeNameTextBox.Size = new System.Drawing.Size(492, 20);
            this.routeNameTextBox.TabIndex = 0;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(12, 12);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(115, 25);
            this.nameLabel.TabIndex = 12;
            this.nameLabel.Text = "RouteName";
            // 
            // EditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 182);
            this.Controls.Add(this.sourceDirectoryTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.destinationDirectoryTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chooseDestinationDirectoryButton);
            this.Controls.Add(this.sourceDirectoryLabel);
            this.Controls.Add(this.chooseSourceDirectoryButton);
            this.Controls.Add(this.routeNameTextBox);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditWindow";
            this.Text = "Edit Route";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceDirectoryTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.TextBox destinationDirectoryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseDestinationDirectoryButton;
        private System.Windows.Forms.Label sourceDirectoryLabel;
        private System.Windows.Forms.Button chooseSourceDirectoryButton;
        private System.Windows.Forms.TextBox routeNameTextBox;
        private System.Windows.Forms.Label nameLabel;
    }
}