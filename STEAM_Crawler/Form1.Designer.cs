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
            this.btnSaveXLS = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSteamInitialLink
            // 
            this.tbSteamInitialLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSteamInitialLink.Location = new System.Drawing.Point(30, 22);
            this.tbSteamInitialLink.Name = "tbSteamInitialLink";
            this.tbSteamInitialLink.Size = new System.Drawing.Size(970, 20);
            this.tbSteamInitialLink.TabIndex = 0;
            this.tbSteamInitialLink.Text = resources.GetString("tbSteamInitialLink.Text");
            // 
            // btnStartBrowser
            // 
            this.btnStartBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartBrowser.Location = new System.Drawing.Point(925, 48);
            this.btnStartBrowser.Name = "btnStartBrowser";
            this.btnStartBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnStartBrowser.TabIndex = 1;
            this.btnStartBrowser.Text = "Open Browser";
            this.btnStartBrowser.UseVisualStyleBackColor = true;
            this.btnStartBrowser.Click += new System.EventHandler(this.btnStartBrowser_Click);
            // 
            // btnStopCrawler
            // 
            this.btnStopCrawler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopCrawler.Location = new System.Drawing.Point(943, 356);
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
            this.GotoNextPage.Location = new System.Drawing.Point(925, 77);
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1003, 238);
            this.dataGridView1.TabIndex = 5;
            // 
            // btnReadList
            // 
            this.btnReadList.Location = new System.Drawing.Point(844, 77);
            this.btnReadList.Name = "btnReadList";
            this.btnReadList.Size = new System.Drawing.Size(75, 23);
            this.btnReadList.TabIndex = 6;
            this.btnReadList.Text = "Get Items";
            this.btnReadList.UseVisualStyleBackColor = true;
            this.btnReadList.Click += new System.EventHandler(this.btnReadList_Click);
            // 
            // btnSaveXLS
            // 
            this.btnSaveXLS.Location = new System.Drawing.Point(844, 48);
            this.btnSaveXLS.Name = "btnSaveXLS";
            this.btnSaveXLS.Size = new System.Drawing.Size(75, 23);
            this.btnSaveXLS.TabIndex = 7;
            this.btnSaveXLS.Text = "Save XLSX";
            this.btnSaveXLS.UseVisualStyleBackColor = true;
            this.btnSaveXLS.Click += new System.EventHandler(this.btnSaveXLS_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1009, 244);
            this.panel1.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1033, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 400);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSaveXLS);
            this.Controls.Add(this.btnReadList);
            this.Controls.Add(this.GotoNextPage);
            this.Controls.Add(this.lblPagesCount);
            this.Controls.Add(this.btnStopCrawler);
            this.Controls.Add(this.btnStartBrowser);
            this.Controls.Add(this.tbSteamInitialLink);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "Form1";
            this.Text = "STEAM CRAWLER";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.Button btnSaveXLS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

