namespace OdtToMm
{
    partial class Form1
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
            this.ODTbtn = new System.Windows.Forms.Button();
            this.MMbtn = new System.Windows.Forms.Button();
            this.Cbtn = new System.Windows.Forms.Button();
            this.ODTtb = new System.Windows.Forms.TextBox();
            this.MMtb = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ODTbtn
            // 
            this.ODTbtn.Location = new System.Drawing.Point(0, 0);
            this.ODTbtn.Name = "ODTbtn";
            this.ODTbtn.Size = new System.Drawing.Size(124, 23);
            this.ODTbtn.TabIndex = 0;
            this.ODTbtn.Text = "Choose ODT input";
            this.ODTbtn.UseVisualStyleBackColor = true;
            this.ODTbtn.Click += new System.EventHandler(this.ODTbtn_Click);
            // 
            // MMbtn
            // 
            this.MMbtn.Location = new System.Drawing.Point(0, 29);
            this.MMbtn.Name = "MMbtn";
            this.MMbtn.Size = new System.Drawing.Size(124, 23);
            this.MMbtn.TabIndex = 1;
            this.MMbtn.Text = "Save as";
            this.MMbtn.UseVisualStyleBackColor = true;
            this.MMbtn.Click += new System.EventHandler(this.MMbtn_Click);
            // 
            // Cbtn
            // 
            this.Cbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Cbtn.Location = new System.Drawing.Point(12, 78);
            this.Cbtn.Name = "Cbtn";
            this.Cbtn.Size = new System.Drawing.Size(492, 74);
            this.Cbtn.TabIndex = 2;
            this.Cbtn.Text = "Convert";
            this.Cbtn.UseVisualStyleBackColor = true;
            this.Cbtn.Click += new System.EventHandler(this.Cbtn_Click);
            // 
            // ODTtb
            // 
            this.ODTtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ODTtb.Location = new System.Drawing.Point(130, 0);
            this.ODTtb.Name = "ODTtb";
            this.ODTtb.ReadOnly = true;
            this.ODTtb.Size = new System.Drawing.Size(374, 20);
            this.ODTtb.TabIndex = 3;
            // 
            // MMtb
            // 
            this.MMtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MMtb.Location = new System.Drawing.Point(130, 31);
            this.MMtb.Name = "MMtb";
            this.MMtb.ReadOnly = true;
            this.MMtb.Size = new System.Drawing.Size(374, 20);
            this.MMtb.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 158);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(112, 13);
            this.progressBar1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Waiting for input";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 182);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.MMtb);
            this.Controls.Add(this.ODTtb);
            this.Controls.Add(this.Cbtn);
            this.Controls.Add(this.MMbtn);
            this.Controls.Add(this.ODTbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ODTbtn;
        private System.Windows.Forms.Button MMbtn;
        private System.Windows.Forms.Button Cbtn;
        private System.Windows.Forms.TextBox ODTtb;
        private System.Windows.Forms.TextBox MMtb;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
    }
}

