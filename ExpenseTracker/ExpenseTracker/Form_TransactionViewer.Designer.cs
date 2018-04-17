namespace ExpenseTracker
{
    partial class Form_TransactionViewer
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
            this.PrevTransactionBtn = new System.Windows.Forms.Button();
            this.NextTransactionBtn = new System.Windows.Forms.Button();
            this.TransactionIDLbl = new System.Windows.Forms.Label();
            this.TransactionInfoLbl = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PrevTransactionBtn
            // 
            this.PrevTransactionBtn.Location = new System.Drawing.Point(176, 54);
            this.PrevTransactionBtn.Name = "PrevTransactionBtn";
            this.PrevTransactionBtn.Size = new System.Drawing.Size(75, 23);
            this.PrevTransactionBtn.TabIndex = 0;
            this.PrevTransactionBtn.Text = "<-- prev";
            this.PrevTransactionBtn.UseVisualStyleBackColor = true;
            this.PrevTransactionBtn.Click += new System.EventHandler(this.PrevTransactionBtn_Click);
            // 
            // NextTransactionBtn
            // 
            this.NextTransactionBtn.Location = new System.Drawing.Point(435, 54);
            this.NextTransactionBtn.Name = "NextTransactionBtn";
            this.NextTransactionBtn.Size = new System.Drawing.Size(75, 23);
            this.NextTransactionBtn.TabIndex = 1;
            this.NextTransactionBtn.Text = "next -->";
            this.NextTransactionBtn.UseVisualStyleBackColor = true;
            this.NextTransactionBtn.Click += new System.EventHandler(this.NextTransactionBtn_Click);
            // 
            // TransactionIDLbl
            // 
            this.TransactionIDLbl.AutoSize = true;
            this.TransactionIDLbl.Location = new System.Drawing.Point(302, 59);
            this.TransactionIDLbl.Name = "TransactionIDLbl";
            this.TransactionIDLbl.Size = new System.Drawing.Size(89, 13);
            this.TransactionIDLbl.TabIndex = 2;
            this.TransactionIDLbl.Text = "-- no transaction--";
            // 
            // TransactionInfoLbl
            // 
            this.TransactionInfoLbl.AutoSize = true;
            this.TransactionInfoLbl.Location = new System.Drawing.Point(302, 134);
            this.TransactionInfoLbl.Name = "TransactionInfoLbl";
            this.TransactionInfoLbl.Size = new System.Drawing.Size(97, 13);
            this.TransactionInfoLbl.TabIndex = 3;
            this.TransactionInfoLbl.Text = "-- transaction info --";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(652, 54);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 4;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // Form_TransactionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.TransactionInfoLbl);
            this.Controls.Add(this.TransactionIDLbl);
            this.Controls.Add(this.NextTransactionBtn);
            this.Controls.Add(this.PrevTransactionBtn);
            this.Name = "Form_TransactionViewer";
            this.Text = "Form_TransactionViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PrevTransactionBtn;
        private System.Windows.Forms.Button NextTransactionBtn;
        private System.Windows.Forms.Label TransactionIDLbl;
        private System.Windows.Forms.Label TransactionInfoLbl;
        private System.Windows.Forms.Button removeButton;
    }
}