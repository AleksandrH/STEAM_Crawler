﻿namespace STEAM_Crawler
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tbSteamInitialLink = new System.Windows.Forms.TextBox();
            this.btnStartBrowser = new System.Windows.Forms.Button();
            this.btnStopCrawler = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnReadList = new System.Windows.Forms.Button();
            this.btnSaveXLS = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PageCounter = new System.Windows.Forms.NumericUpDown();
            this.lblPagesCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemsMinimum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSteamInitialLink
            // 
            this.tbSteamInitialLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSteamInitialLink.Location = new System.Drawing.Point(18, 25);
            this.tbSteamInitialLink.Multiline = true;
            this.tbSteamInitialLink.Name = "tbSteamInitialLink";
            this.tbSteamInitialLink.Size = new System.Drawing.Size(1006, 43);
            this.tbSteamInitialLink.TabIndex = 0;
            this.tbSteamInitialLink.Text = resources.GetString("tbSteamInitialLink.Text");
            // 
            // btnStartBrowser
            // 
            this.btnStartBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartBrowser.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartBrowser.Location = new System.Drawing.Point(670, 91);
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
            this.btnStopCrawler.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStopCrawler.Location = new System.Drawing.Point(915, 390);
            this.btnStopCrawler.Name = "btnStopCrawler";
            this.btnStopCrawler.Size = new System.Drawing.Size(103, 23);
            this.btnStopCrawler.TabIndex = 2;
            this.btnStopCrawler.Text = "exit browser";
            this.btnStopCrawler.UseVisualStyleBackColor = true;
            this.btnStopCrawler.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1003, 249);
            this.dataGridView1.TabIndex = 5;
            // 
            // btnReadList
            // 
            this.btnReadList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadList.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReadList.Location = new System.Drawing.Point(757, 91);
            this.btnReadList.Name = "btnReadList";
            this.btnReadList.Size = new System.Drawing.Size(75, 23);
            this.btnReadList.TabIndex = 6;
            this.btnReadList.Text = "Get Items";
            this.btnReadList.UseVisualStyleBackColor = true;
            this.btnReadList.Click += new System.EventHandler(this.btnReadList_Click);
            // 
            // btnSaveXLS
            // 
            this.btnSaveXLS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveXLS.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveXLS.Location = new System.Drawing.Point(931, 91);
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
            this.panel1.Location = new System.Drawing.Point(12, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1009, 255);
            this.panel1.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(18, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pages Founded:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(18, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Start Link:";
            // 
            // PageCounter
            // 
            this.PageCounter.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PageCounter.Location = new System.Drawing.Point(123, 92);
            this.PageCounter.Name = "PageCounter";
            this.PageCounter.Size = new System.Drawing.Size(32, 21);
            this.PageCounter.TabIndex = 12;
            // 
            // lblPagesCount
            // 
            this.lblPagesCount.AutoSize = true;
            this.lblPagesCount.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPagesCount.Location = new System.Drawing.Point(123, 73);
            this.lblPagesCount.Name = "lblPagesCount";
            this.lblPagesCount.Size = new System.Drawing.Size(14, 16);
            this.lblPagesCount.TabIndex = 13;
            this.lblPagesCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(18, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Pages work with:";
            // 
            // ItemsMinimum
            // 
            this.ItemsMinimum.Location = new System.Drawing.Point(279, 69);
            this.ItemsMinimum.Name = "ItemsMinimum";
            this.ItemsMinimum.Size = new System.Drawing.Size(35, 20);
            this.ItemsMinimum.TabIndex = 15;
            this.ItemsMinimum.Text = "200";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(186, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Items Starts At :";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancel.Location = new System.Drawing.Point(844, 91);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 17;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Clear datagrid";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 434);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ItemsMinimum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPagesCount);
            this.Controls.Add(this.PageCounter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSaveXLS);
            this.Controls.Add(this.btnReadList);
            this.Controls.Add(this.btnStopCrawler);
            this.Controls.Add(this.btnStartBrowser);
            this.Controls.Add(this.tbSteamInitialLink);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "frmMain";
            this.Text = "STEAM CRAWLER";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSteamInitialLink;
        private System.Windows.Forms.Button btnStartBrowser;
        private System.Windows.Forms.Button btnStopCrawler;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnReadList;
        private System.Windows.Forms.Button btnSaveXLS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PageCounter;
        private System.Windows.Forms.Label lblPagesCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ItemsMinimum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button button1;
    }
}

