namespace Loodon
{
    partial class FrmCreateReports
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgrdTables = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgrdColumns = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgrdSelectedColumns = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPreviewandExport = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdTables)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdColumns)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdSelectedColumns)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgrdTables);
            this.groupBox2.Location = new System.Drawing.Point(2, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 430);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Table(s)";
            // 
            // dgrdTables
            // 
            this.dgrdTables.AllowUserToAddRows = false;
            this.dgrdTables.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgrdTables.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrdTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdTables.ColumnHeadersVisible = false;
            this.dgrdTables.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgrdTables.Location = new System.Drawing.Point(6, 17);
            this.dgrdTables.Name = "dgrdTables";
            this.dgrdTables.RowHeadersVisible = false;
            this.dgrdTables.Size = new System.Drawing.Size(187, 407);
            this.dgrdTables.TabIndex = 1;
            this.dgrdTables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdTables_CellClick);
            this.dgrdTables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdTables_CellContentClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgrdColumns);
            this.groupBox3.Location = new System.Drawing.Point(204, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 430);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Column(s)";
            // 
            // dgrdColumns
            // 
            this.dgrdColumns.AllowUserToAddRows = false;
            this.dgrdColumns.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgrdColumns.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgrdColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrdColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdColumns.ColumnHeadersVisible = false;
            this.dgrdColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgrdColumns.Location = new System.Drawing.Point(8, 17);
            this.dgrdColumns.Name = "dgrdColumns";
            this.dgrdColumns.RowHeadersVisible = false;
            this.dgrdColumns.Size = new System.Drawing.Size(187, 407);
            this.dgrdColumns.TabIndex = 1;
            this.dgrdColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgrdColumns_MouseDown);
            this.dgrdColumns.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdColumns_CellDoubleClick);
            this.dgrdColumns.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgrdColumns_MouseMove);
            this.dgrdColumns.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgrdColumns_MouseClick);
            this.dgrdColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.dgrdColumns_DragOver);
            this.dgrdColumns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgrdColumns_MouseUp);
            this.dgrdColumns.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdColumns_CellClick);
            this.dgrdColumns.DragLeave += new System.EventHandler(this.dgrdColumns_DragLeave);
            this.dgrdColumns.Click += new System.EventHandler(this.dgrdColumns_Click);
            this.dgrdColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgrdColumns_DragDrop);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgrdSelectedColumns);
            this.groupBox4.Location = new System.Drawing.Point(406, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 430);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selected Column(s)";
            // 
            // dgrdSelectedColumns
            // 
            this.dgrdSelectedColumns.AllowUserToAddRows = false;
            this.dgrdSelectedColumns.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgrdSelectedColumns.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgrdSelectedColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrdSelectedColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdSelectedColumns.ColumnHeadersVisible = false;
            this.dgrdSelectedColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgrdSelectedColumns.Location = new System.Drawing.Point(7, 17);
            this.dgrdSelectedColumns.Name = "dgrdSelectedColumns";
            this.dgrdSelectedColumns.RowHeadersVisible = false;
            this.dgrdSelectedColumns.Size = new System.Drawing.Size(187, 407);
            this.dgrdSelectedColumns.TabIndex = 2;
            this.dgrdSelectedColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.dgrdSelectedColumns_DragOver);
            this.dgrdSelectedColumns.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgrdSelectedColumns_DragEnter);
            this.dgrdSelectedColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgrdSelectedColumns_DragDrop);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(527, 437);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectColumnToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 26);
            // 
            // selectColumnToolStripMenuItem
            // 
            this.selectColumnToolStripMenuItem.Name = "selectColumnToolStripMenuItem";
            this.selectColumnToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.selectColumnToolStripMenuItem.Text = "Select Column";
            this.selectColumnToolStripMenuItem.Click += new System.EventHandler(this.selectColumnToolStripMenuItem_Click);
            // 
            // btnPreviewandExport
            // 
            this.btnPreviewandExport.Location = new System.Drawing.Point(406, 437);
            this.btnPreviewandExport.Name = "btnPreviewandExport";
            this.btnPreviewandExport.Size = new System.Drawing.Size(115, 23);
            this.btnPreviewandExport.TabIndex = 7;
            this.btnPreviewandExport.Text = "Preview and Export";
            this.btnPreviewandExport.UseVisualStyleBackColor = true;
            this.btnPreviewandExport.Click += new System.EventHandler(this.btnPreviewandExport_Click);
            // 
            // frmCreateReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(609, 466);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnPreviewandExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCreateReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Reports";
            this.Load += new System.EventHandler(this.frmCreateReports_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdTables)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdColumns)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdSelectedColumns)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgrdTables;
        private System.Windows.Forms.DataGridView dgrdColumns;
        private System.Windows.Forms.DataGridView dgrdSelectedColumns;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectColumnToolStripMenuItem;
        private System.Windows.Forms.Button btnPreviewandExport;
    }
}