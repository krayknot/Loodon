namespace Loodon
{
    partial class FrmAttachDatabase
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseldf = new System.Windows.Forms.Button();
            this.btnBrowsemdf = new System.Windows.Forms.Button();
            this.txtDatabaseldf = new System.Windows.Forms.TextBox();
            this.txtDatabasemdf = new System.Windows.Forms.TextBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAttach = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowseldf);
            this.groupBox1.Controls.Add(this.btnBrowsemdf);
            this.groupBox1.Controls.Add(this.txtDatabaseldf);
            this.groupBox1.Controls.Add(this.txtDatabasemdf);
            this.groupBox1.Controls.Add(this.txtDatabaseName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Destination";
            // 
            // btnBrowseldf
            // 
            this.btnBrowseldf.Location = new System.Drawing.Point(337, 70);
            this.btnBrowseldf.Name = "btnBrowseldf";
            this.btnBrowseldf.Size = new System.Drawing.Size(29, 23);
            this.btnBrowseldf.TabIndex = 6;
            this.btnBrowseldf.Text = "...";
            this.btnBrowseldf.UseVisualStyleBackColor = true;
            this.btnBrowseldf.Click += new System.EventHandler(this.btnBrowseldf_Click);
            // 
            // btnBrowsemdf
            // 
            this.btnBrowsemdf.Location = new System.Drawing.Point(337, 46);
            this.btnBrowsemdf.Name = "btnBrowsemdf";
            this.btnBrowsemdf.Size = new System.Drawing.Size(29, 23);
            this.btnBrowsemdf.TabIndex = 1;
            this.btnBrowsemdf.Text = "...";
            this.btnBrowsemdf.UseVisualStyleBackColor = true;
            this.btnBrowsemdf.Click += new System.EventHandler(this.btnBrowsemdf_Click);
            // 
            // txtDatabaseldf
            // 
            this.txtDatabaseldf.Location = new System.Drawing.Point(132, 72);
            this.txtDatabaseldf.Name = "txtDatabaseldf";
            this.txtDatabaseldf.Size = new System.Drawing.Size(199, 21);
            this.txtDatabaseldf.TabIndex = 5;
            // 
            // txtDatabasemdf
            // 
            this.txtDatabasemdf.Location = new System.Drawing.Point(132, 48);
            this.txtDatabasemdf.Name = "txtDatabasemdf";
            this.txtDatabasemdf.Size = new System.Drawing.Size(199, 21);
            this.txtDatabasemdf.TabIndex = 4;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(132, 14);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(157, 21);
            this.txtDatabaseName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Database log file [.ldf]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database file [.mdf]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database Name:";
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(221, 120);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(75, 23);
            this.btnAttach.TabIndex = 1;
            this.btnAttach.Text = "&Attach";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(302, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmAttachDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(389, 152);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAttach);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAttachDatabase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Attach Database";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseldf;
        private System.Windows.Forms.Button btnBrowsemdf;
        private System.Windows.Forms.TextBox txtDatabaseldf;
        private System.Windows.Forms.TextBox txtDatabasemdf;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}