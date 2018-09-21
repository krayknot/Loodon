namespace Loodon
{
    partial class FrmLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLicense));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNotnow = new System.Windows.Forms.Button();
            this.btnDontAgree = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAgree = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNotnow);
            this.groupBox1.Controls.Add(this.btnDontAgree);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btnAgree);
            this.groupBox1.Location = new System.Drawing.Point(4, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 404);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnNotnow
            // 
            this.btnNotnow.BackColor = System.Drawing.Color.Gray;
            this.btnNotnow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotnow.ForeColor = System.Drawing.Color.White;
            this.btnNotnow.Location = new System.Drawing.Point(6, 360);
            this.btnNotnow.Name = "btnNotnow";
            this.btnNotnow.Size = new System.Drawing.Size(522, 39);
            this.btnNotnow.TabIndex = 2;
            this.btnNotnow.Text = "Not now";
            this.btnNotnow.UseVisualStyleBackColor = false;
            this.btnNotnow.Click += new System.EventHandler(this.btnNotnow_Click);
            // 
            // btnDontAgree
            // 
            this.btnDontAgree.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnDontAgree.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDontAgree.ForeColor = System.Drawing.Color.White;
            this.btnDontAgree.Location = new System.Drawing.Point(6, 317);
            this.btnDontAgree.Name = "btnDontAgree";
            this.btnDontAgree.Size = new System.Drawing.Size(522, 39);
            this.btnDontAgree.TabIndex = 1;
            this.btnDontAgree.Text = "I don\'t agree";
            this.btnDontAgree.UseVisualStyleBackColor = false;
            this.btnDontAgree.Click += new System.EventHandler(this.btnDontAgree_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 11);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(519, 259);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // btnAgree
            // 
            this.btnAgree.BackColor = System.Drawing.Color.Black;
            this.btnAgree.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgree.ForeColor = System.Drawing.Color.White;
            this.btnAgree.Location = new System.Drawing.Point(5, 276);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(522, 39);
            this.btnAgree.TabIndex = 0;
            this.btnAgree.Text = "I agree to the following Terms and Conditions";
            this.btnAgree.UseVisualStyleBackColor = false;
            this.btnAgree.Click += new System.EventHandler(this.btnAgree_Click);
            // 
            // frmLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 408);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Coral;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLicense";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terms and Conditions ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAgree;
        private System.Windows.Forms.Button btnDontAgree;
        private System.Windows.Forms.Button btnNotnow;
    }
}