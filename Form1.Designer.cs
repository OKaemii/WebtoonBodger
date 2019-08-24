namespace WebtoonBodge
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
            this.LBX_images = new System.Windows.Forms.ListBox();
            this.BTN_refresh = new System.Windows.Forms.Button();
            this.TB_maxHeight = new System.Windows.Forms.TextBox();
            this.LB_maxHeight = new System.Windows.Forms.Label();
            this.BTN_init = new System.Windows.Forms.Button();
            this.RB_isPNG = new System.Windows.Forms.RadioButton();
            this.RB_isJPG = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // LBX_images
            // 
            this.LBX_images.FormattingEnabled = true;
            this.LBX_images.ItemHeight = 16;
            this.LBX_images.Location = new System.Drawing.Point(12, 76);
            this.LBX_images.Name = "LBX_images";
            this.LBX_images.Size = new System.Drawing.Size(276, 356);
            this.LBX_images.TabIndex = 0;
            // 
            // BTN_refresh
            // 
            this.BTN_refresh.Location = new System.Drawing.Point(213, 47);
            this.BTN_refresh.Name = "BTN_refresh";
            this.BTN_refresh.Size = new System.Drawing.Size(75, 23);
            this.BTN_refresh.TabIndex = 1;
            this.BTN_refresh.Text = "refresh";
            this.BTN_refresh.UseVisualStyleBackColor = true;
            this.BTN_refresh.Click += new System.EventHandler(this.BTN_refresh_Click);
            // 
            // TB_maxHeight
            // 
            this.TB_maxHeight.Location = new System.Drawing.Point(294, 410);
            this.TB_maxHeight.Name = "TB_maxHeight";
            this.TB_maxHeight.Size = new System.Drawing.Size(127, 22);
            this.TB_maxHeight.Text = "1200";
            this.TB_maxHeight.TabIndex = 2;
			// 
			// LB_maxHeight
			// 
			this.LB_maxHeight.AutoSize = true;
            this.LB_maxHeight.Location = new System.Drawing.Point(294, 390);
            this.LB_maxHeight.Name = "Lb_maxHeight";
            this.LB_maxHeight.Size = new System.Drawing.Size(46, 17);
            this.LB_maxHeight.TabIndex = 3;
            this.LB_maxHeight.Text = "max height";
			// 
			// BTN_init
			// 
			this.BTN_init.Location = new System.Drawing.Point(297, 47);
            this.BTN_init.Name = "BTN_init";
            this.BTN_init.Size = new System.Drawing.Size(124, 329);
            this.BTN_init.TabIndex = 4;
            this.BTN_init.Text = "Init";
            this.BTN_init.UseVisualStyleBackColor = true;
            this.BTN_init.Click += new System.EventHandler(this.BTN_init_Click);
			// 
			// RB_isPNG
			// 
			this.RB_isPNG.AutoSize = true;
            this.RB_isPNG.Location = new System.Drawing.Point(13, 47);
            this.RB_isPNG.Name = "RB_isPNG";
            this.RB_isPNG.Size = new System.Drawing.Size(110, 21);
            this.RB_isPNG.TabIndex = 5;
            this.RB_isPNG.TabStop = true;
            this.RB_isPNG.Text = "PNG";
            this.RB_isPNG.UseVisualStyleBackColor = true;
            this.RB_isPNG.CheckedChanged += new System.EventHandler(this.RB_isJPG_CheckedChanged);
			// 
			// RB_isJPG
			// 
			this.RB_isJPG.AutoSize = true;
            this.RB_isJPG.Location = new System.Drawing.Point(13, 20);
            this.RB_isJPG.Name = "RB_isJPG";
            this.RB_isJPG.Size = new System.Drawing.Size(110, 21);
            this.RB_isJPG.TabIndex = 6;
            this.RB_isJPG.TabStop = true;
            this.RB_isJPG.Text = "JPG";
            this.RB_isJPG.UseVisualStyleBackColor = true;
            this.RB_isJPG.CheckedChanged += new System.EventHandler(this.RB_isPNG_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 450);
            this.Controls.Add(this.RB_isJPG);
            this.Controls.Add(this.RB_isPNG);
            this.Controls.Add(this.BTN_init);
            this.Controls.Add(this.LB_maxHeight);
            this.Controls.Add(this.TB_maxHeight);
            this.Controls.Add(this.BTN_refresh);
            this.Controls.Add(this.LBX_images);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "WebtoonBodger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LBX_images;
        private System.Windows.Forms.Button BTN_refresh;
        private System.Windows.Forms.TextBox TB_maxHeight;
        private System.Windows.Forms.Label LB_maxHeight;
        private System.Windows.Forms.Button BTN_init;
        private System.Windows.Forms.RadioButton RB_isPNG;
        private System.Windows.Forms.RadioButton RB_isJPG;
    }
}

