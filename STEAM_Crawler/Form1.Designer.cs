namespace STEAM_Crawler
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbSteamInitialLink = new System.Windows.Forms.TextBox();
            this.btnStartBrowser = new System.Windows.Forms.Button();
            this.btnStopCrawler = new System.Windows.Forms.Button();
            this.lblPagesCount = new System.Windows.Forms.Label();
            this.GotoNextPage = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnReadList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSteamInitialLink
            // 
            this.tbSteamInitialLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSteamInitialLink.Location = new System.Drawing.Point(30, 22);
            this.tbSteamInitialLink.Name = "tbSteamInitialLink";
            this.tbSteamInitialLink.Size = new System.Drawing.Size(533, 20);
            this.tbSteamInitialLink.TabIndex = 0;
            this.tbSteamInitialLink.Text = resources.GetString("tbSteamInitialLink.Text");
            // 
            // btnStartBrowser
            // 
            this.btnStartBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartBrowser.Location = new System.Drawing.Point(488, 48);
            this.btnStartBrowser.Name = "btnStartBrowser";
            this.btnStartBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnStartBrowser.TabIndex = 1;
            this.btnStartBrowser.Text = "Open Browser";
            this.btnStartBrowser.UseVisualStyleBackColor = true;
            this.btnStartBrowser.Click += new System.EventHandler(this.btnGetStat_Click);
            // 
            // btnStopCrawler
            // 
            this.btnStopCrawler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopCrawler.Location = new System.Drawing.Point(503, 306);
            this.btnStopCrawler.Name = "btnStopCrawler";
            this.btnStopCrawler.Size = new System.Drawing.Size(75, 23);
            this.btnStopCrawler.TabIndex = 2;
            this.btnStopCrawler.Text = "exit browser";
            this.btnStopCrawler.UseVisualStyleBackColor = true;
            this.btnStopCrawler.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblPagesCount
            // 
            this.lblPagesCount.AutoSize = true;
            this.lblPagesCount.Location = new System.Drawing.Point(35, 55);
            this.lblPagesCount.Name = "lblPagesCount";
            this.lblPagesCount.Size = new System.Drawing.Size(35, 13);
            this.lblPagesCount.TabIndex = 3;
            this.lblPagesCount.Text = "label1";
            // 
            // GotoNextPage
            // 
            this.GotoNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GotoNextPage.Location = new System.Drawing.Point(488, 77);
            this.GotoNextPage.Name = "GotoNextPage";
            this.GotoNextPage.Size = new System.Drawing.Size(75, 23);
            this.GotoNextPage.TabIndex = 4;
            this.GotoNextPage.Text = "Next Page";
            this.GotoNextPage.UseVisualStyleBackColor = true;
            this.GotoNextPage.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(572, 194);
            this.dataGridView1.TabIndex = 5;
            // 
            // btnReadList
            // 
            this.btnReadList.Location = new System.Drawing.Point(346, 77);
            this.btnReadList.Name = "btnReadList";
            this.btnReadList.Size = new System.Drawing.Size(75, 23);
            this.btnReadList.TabIndex = 6;
            this.btnReadList.Text = "Get Items";
            this.btnReadList.UseVisualStyleBackColor = true;
            this.btnReadList.Click += new System.EventHandler(this.btnReadList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 336);
            this.Controls.Add(this.btnReadList);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.GotoNextPage);
            this.Controls.Add(this.lblPagesCount);
            this.Controls.Add(this.btnStopCrawler);
            this.Controls.Add(this.btnStartBrowser);
            this.Controls.Add(this.tbSteamInitialLink);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "Form1";
            this.Text = "STEAM CRAWLER";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSteamInitialLink;
        private System.Windows.Forms.Button btnStartBrowser;
        private System.Windows.Forms.Button btnStopCrawler;
        private System.Windows.Forms.Label lblPagesCount;
        private System.Windows.Forms.Button GotoNextPage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnReadList;
    }
}

