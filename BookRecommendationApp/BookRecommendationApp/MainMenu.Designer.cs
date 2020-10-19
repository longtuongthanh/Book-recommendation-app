namespace BookRecommendationApp
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butHelp = new System.Windows.Forms.Button();
            this.butSetting = new System.Windows.Forms.Button();
            this.butAcc = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butSearch = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.butMybooks = new System.Windows.Forms.Button();
            this.butMax = new System.Windows.Forms.Button();
            this.butHome = new System.Windows.Forms.Button();
            this.butMin = new System.Windows.Forms.Button();
            this.panelLoad = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(399, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(279, 26);
            this.textBox1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.butHelp);
            this.panel2.Controls.Add(this.butSetting);
            this.panel2.Controls.Add(this.butAcc);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.butSearch);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.butExit);
            this.panel2.Controls.Add(this.butMybooks);
            this.panel2.Controls.Add(this.butMax);
            this.panel2.Controls.Add(this.butHome);
            this.panel2.Controls.Add(this.butMin);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(975, 65);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(694, 8);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 49);
            this.panel3.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(866, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 49);
            this.panel1.TabIndex = 4;
            // 
            // butHelp
            // 
            this.butHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butHelp.FlatAppearance.BorderSize = 0;
            this.butHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butHelp.Image = global::BookRecommendationApp.Properties.Resources.doubt__1_;
            this.butHelp.Location = new System.Drawing.Point(799, 0);
            this.butHelp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butHelp.Name = "butHelp";
            this.butHelp.Size = new System.Drawing.Size(45, 65);
            this.butHelp.TabIndex = 4;
            this.butHelp.UseVisualStyleBackColor = true;
            this.butHelp.Click += new System.EventHandler(this.butHelp_Click);
            // 
            // butSetting
            // 
            this.butSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSetting.FlatAppearance.BorderSize = 0;
            this.butSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSetting.Image = global::BookRecommendationApp.Properties.Resources.levels__1_;
            this.butSetting.Location = new System.Drawing.Point(700, 0);
            this.butSetting.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butSetting.Name = "butSetting";
            this.butSetting.Size = new System.Drawing.Size(45, 65);
            this.butSetting.TabIndex = 4;
            this.butSetting.UseVisualStyleBackColor = true;
            this.butSetting.Click += new System.EventHandler(this.butSetting_Click);
            // 
            // butAcc
            // 
            this.butAcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAcc.FlatAppearance.BorderSize = 0;
            this.butAcc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAcc.Image = global::BookRecommendationApp.Properties.Resources.user__2_;
            this.butAcc.Location = new System.Drawing.Point(749, 0);
            this.butAcc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butAcc.Name = "butAcc";
            this.butAcc.Size = new System.Drawing.Size(45, 65);
            this.butAcc.TabIndex = 4;
            this.butAcc.UseVisualStyleBackColor = true;
            this.butAcc.Click += new System.EventHandler(this.butAcc_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Image = global::BookRecommendationApp.Properties.Resources.book__4_1;
            this.pictureBox1.Location = new System.Drawing.Point(9, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // butSearch
            // 
            this.butSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSearch.BackColor = System.Drawing.Color.White;
            this.butSearch.FlatAppearance.BorderSize = 0;
            this.butSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSearch.Image = global::BookRecommendationApp.Properties.Resources.magnifiying_glass;
            this.butSearch.Location = new System.Drawing.Point(655, 21);
            this.butSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(21, 23);
            this.butSearch.TabIndex = 10;
            this.butSearch.UseVisualStyleBackColor = false;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // butExit
            // 
            this.butExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butExit.FlatAppearance.BorderSize = 0;
            this.butExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butExit.Image = global::BookRecommendationApp.Properties.Resources.close__2_;
            this.butExit.Location = new System.Drawing.Point(947, 19);
            this.butExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(26, 28);
            this.butExit.TabIndex = 7;
            this.butExit.UseVisualStyleBackColor = true;
            this.butExit.Click += new System.EventHandler(this.butExit_Click_1);
            // 
            // butMybooks
            // 
            this.butMybooks.BackColor = System.Drawing.SystemColors.ControlLight;
            this.butMybooks.FlatAppearance.BorderSize = 0;
            this.butMybooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butMybooks.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butMybooks.Image = global::BookRecommendationApp.Properties.Resources.photo_album;
            this.butMybooks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butMybooks.Location = new System.Drawing.Point(226, 0);
            this.butMybooks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butMybooks.Name = "butMybooks";
            this.butMybooks.Size = new System.Drawing.Size(148, 65);
            this.butMybooks.TabIndex = 4;
            this.butMybooks.Text = "      MY BOOKS";
            this.butMybooks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butMybooks.UseVisualStyleBackColor = false;
            this.butMybooks.Click += new System.EventHandler(this.butMybooks_Click);
            // 
            // butMax
            // 
            this.butMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butMax.FlatAppearance.BorderSize = 0;
            this.butMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butMax.Image = global::BookRecommendationApp.Properties.Resources.full_size__1_;
            this.butMax.Location = new System.Drawing.Point(918, 19);
            this.butMax.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butMax.Name = "butMax";
            this.butMax.Size = new System.Drawing.Size(26, 28);
            this.butMax.TabIndex = 8;
            this.butMax.UseVisualStyleBackColor = true;
            this.butMax.Click += new System.EventHandler(this.butMax_Click_1);
            // 
            // butHome
            // 
            this.butHome.BackColor = System.Drawing.SystemColors.ControlLight;
            this.butHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butHome.FlatAppearance.BorderSize = 0;
            this.butHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butHome.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butHome.Image = global::BookRecommendationApp.Properties.Resources.home__3_;
            this.butHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butHome.Location = new System.Drawing.Point(104, 0);
            this.butHome.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butHome.Name = "butHome";
            this.butHome.Size = new System.Drawing.Size(117, 65);
            this.butHome.TabIndex = 4;
            this.butHome.Text = "      HOME";
            this.butHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butHome.UseVisualStyleBackColor = false;
            this.butHome.Click += new System.EventHandler(this.butHome_Click);
            // 
            // butMin
            // 
            this.butMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butMin.FlatAppearance.BorderSize = 0;
            this.butMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butMin.Image = global::BookRecommendationApp.Properties.Resources.minimize__1_;
            this.butMin.Location = new System.Drawing.Point(889, 19);
            this.butMin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butMin.Name = "butMin";
            this.butMin.Size = new System.Drawing.Size(26, 28);
            this.butMin.TabIndex = 9;
            this.butMin.UseVisualStyleBackColor = true;
            this.butMin.Click += new System.EventHandler(this.butMin_Click_1);
            // 
            // panelLoad
            // 
            this.panelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLoad.Location = new System.Drawing.Point(0, 65);
            this.panelLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelLoad.Name = "panelLoad";
            this.panelLoad.Size = new System.Drawing.Size(975, 504);
            this.panelLoad.TabIndex = 4;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(975, 569);
            this.Controls.Add(this.panelLoad);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butHome;
        private System.Windows.Forms.Button butMybooks;
        private System.Windows.Forms.Button butMin;
        private System.Windows.Forms.Button butMax;
        private System.Windows.Forms.Button butExit;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button butSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butAcc;
        private System.Windows.Forms.Button butHelp;
        private System.Windows.Forms.Button butSetting;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelLoad;
    }
}

