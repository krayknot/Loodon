namespace Loodon
{
    partial class FrmHorizontalRow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgrdRow = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPRColumn = new System.Windows.Forms.Label();
            this.lblPRKey = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdRow)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrdRow
            // 
            this.dgrdRow.AllowUserToAddRows = false;
            this.dgrdRow.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgrdRow.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgrdRow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrdRow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdRow.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgrdRow.Location = new System.Drawing.Point(11, 20);
            this.dgrdRow.Name = "dgrdRow";
            this.dgrdRow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdRow.Size = new System.Drawing.Size(367, 428);
            this.dgrdRow.TabIndex = 0;
            this.dgrdRow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgrdRow);
            this.groupBox1.Location = new System.Drawing.Point(1, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 454);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Row Details";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(304, 472);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPRColumn
            // 
            this.lblPRColumn.AutoSize = true;
            this.lblPRColumn.Location = new System.Drawing.Point(9, 457);
            this.lblPRColumn.Name = "lblPRColumn";
            this.lblPRColumn.Size = new System.Drawing.Size(105, 13);
            this.lblPRColumn.TabIndex = 2;
            this.lblPRColumn.Text = "Primary key Column:";
            // 
            // lblPRKey
            // 
            this.lblPRKey.AutoSize = true;
            this.lblPRKey.Location = new System.Drawing.Point(9, 472);
            this.lblPRKey.Name = "lblPRKey";
            this.lblPRKey.Size = new System.Drawing.Size(96, 13);
            this.lblPRKey.TabIndex = 3;
            this.lblPRKey.Text = "Primary key value:";
            // 
            // frmHorizontalRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(393, 506);
            this.Controls.Add(this.lblPRKey);
            this.Controls.Add(this.lblPRColumn);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHorizontalRow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Row Details";
            this.Load += new System.EventHandler(this.frmHorizontalRow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdRow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdRow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblPRColumn;
        private System.Windows.Forms.Label lblPRKey;
    }
}