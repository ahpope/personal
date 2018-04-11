namespace _376
{
    partial class Form2
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
            this.employeeListButton = new System.Windows.Forms.Button();
            this.employeeLookUpButton = new System.Windows.Forms.Button();
            this.employeeAddButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.lookUpTextBox = new System.Windows.Forms.TextBox();
            this.randomLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // employeeListButton
            // 
            this.employeeListButton.Location = new System.Drawing.Point(13, 40);
            this.employeeListButton.Name = "employeeListButton";
            this.employeeListButton.Size = new System.Drawing.Size(75, 55);
            this.employeeListButton.TabIndex = 0;
            this.employeeListButton.Text = "View Employee List";
            this.employeeListButton.UseVisualStyleBackColor = true;
            this.employeeListButton.Click += new System.EventHandler(this.employeeListButton_Click);
            // 
            // employeeLookUpButton
            // 
            this.employeeLookUpButton.Location = new System.Drawing.Point(12, 114);
            this.employeeLookUpButton.Name = "employeeLookUpButton";
            this.employeeLookUpButton.Size = new System.Drawing.Size(75, 35);
            this.employeeLookUpButton.TabIndex = 1;
            this.employeeLookUpButton.Text = "Look Up Employee";
            this.employeeLookUpButton.UseVisualStyleBackColor = true;
            this.employeeLookUpButton.Click += new System.EventHandler(this.employeeLookUpButton_Click);
            // 
            // employeeAddButton
            // 
            this.employeeAddButton.Location = new System.Drawing.Point(13, 155);
            this.employeeAddButton.Name = "employeeAddButton";
            this.employeeAddButton.Size = new System.Drawing.Size(75, 37);
            this.employeeAddButton.TabIndex = 2;
            this.employeeAddButton.Text = "Add Employee";
            this.employeeAddButton.UseVisualStyleBackColor = true;
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(230, 12);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(120, 95);
            this.listBox.TabIndex = 3;
            // 
            // lookUpTextBox
            // 
            this.lookUpTextBox.Location = new System.Drawing.Point(94, 123);
            this.lookUpTextBox.Name = "lookUpTextBox";
            this.lookUpTextBox.Size = new System.Drawing.Size(100, 20);
            this.lookUpTextBox.TabIndex = 4;
            // 
            // randomLabel
            // 
            this.randomLabel.AutoSize = true;
            this.randomLabel.Location = new System.Drawing.Point(270, 267);
            this.randomLabel.Name = "randomLabel";
            this.randomLabel.Size = new System.Drawing.Size(35, 13);
            this.randomLabel.TabIndex = 5;
            this.randomLabel.Text = "label1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 517);
            this.Controls.Add(this.randomLabel);
            this.Controls.Add(this.lookUpTextBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.employeeAddButton);
            this.Controls.Add(this.employeeLookUpButton);
            this.Controls.Add(this.employeeListButton);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button employeeListButton;
        private System.Windows.Forms.Button employeeLookUpButton;
        private System.Windows.Forms.Button employeeAddButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.TextBox lookUpTextBox;
        private System.Windows.Forms.Label randomLabel;
    }
}