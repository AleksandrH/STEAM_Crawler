using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace STEAM_Crawler
{
    public partial class Form1 : Form
    {
        IWebDriver browser;
        public static int PAGECOUNT;
        public static DataTable dtDgv;
        public static BindingSource source = new BindingSource();
        public BackgroundWorker myWorker = new BackgroundWorker();
        public Form1()
        {
            InitializeComponent();

            myWorker.DoWork += myWorker_DoWork;
            myWorker.RunWorkerCompleted += myWorker_RunWorkerCompleted;
            myWorker.ProgressChanged += myWorker_ProgressChanged;
            myWorker.WorkerReportsProgress = true;
            myWorker.WorkerSupportsCancellation = true;

            
            dtDgv = new DataTable("dtdgv");

            dtDgv.Columns.Add("ITEM_NAME", typeof(string));
            dtDgv.Columns.Add("ITEM_LINK", typeof(string));
            dtDgv.Columns.Add("ITEM_AT_MRKT", typeof(string));
            dtDgv.Columns.Add("ITEM_MAX_PRICE", typeof(double));
            dtDgv.Columns.Add("ITEM_MIN_PRICE", typeof(double));
            dtDgv.Columns.Add("ITEM_AVG_PRICE", typeof(double));
            dtDgv.Columns.Add("ITEM_AVG_SALES", typeof(double));
            source.DataSource = dtDgv;
            dataGridView1.DataSource = source;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        protected void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;
            //market_listing_row_link
            for (int iPage = 1; iPage <= PAGECOUNT; iPage++)
            {

                var selector = By.ClassName("market_listing_row_link");
                WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
                ww.Until(ExpectedConditions.ElementIsVisible(selector));

                List<IWebElement> results = browser.FindElements(By.ClassName("market_listing_row_link")).ToList();
                int rInd = 1;
                foreach (IWebElement result in results)
                {

                    if (!sendingWorker.CancellationPending)
                    {
                        try
                        {


                            IWebElement item = result.FindElement(By.ClassName("market_listing_searchresult"));
                            IWebElement itemName = item.FindElement(By.ClassName("market_listing_item_name_block"));
                            IWebElement sName = itemName.FindElement(By.ClassName("market_listing_item_name"));
                            IWebElement qtyItem = result.FindElement(By.ClassName("market_listing_num_listings_qty"));

                            List<Operation> Operations = new List<Operation>();
                            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
                            {

                                string htmlCode = client.DownloadString(result.GetAttribute("href"));
                                string strStartCriteria = "var line1=[";
                                string strEndCriteria = "g_timePriceHistoryEarliest";
                                int startPosition = htmlCode.IndexOf(strStartCriteria);
                                int endPosition = htmlCode.IndexOf(strEndCriteria, startPosition);
                                int substrLength = endPosition - (strStartCriteria.Length + startPosition);
                                string stat = htmlCode.Substring((startPosition + strStartCriteria.Length), substrLength);
                                stat = stat.Replace("\t", "").Replace("\n", "");
                                string[] cells = stat.Split(new string[] { "],[" }, StringSplitOptions.None);

                                Array.Reverse(cells);

                                int newArraySize = 30 * 24 > cells.Length ? cells.Length : 30 * 24;
                                string[] lastMonthStat = new string[newArraySize];
                                Array.Copy(cells, 0, lastMonthStat, 0, newArraySize);

                                Operation operation = new Operation();
                                foreach (string hourStat in lastMonthStat)
                                {
                                    string[] oneHourStat = hourStat.Split(',');
                                    Double.TryParse(oneHourStat[1].Replace('.', ','), out operation.OperationPrice);
                                    string tmpAmount = oneHourStat[2];
                                    tmpAmount = tmpAmount.Substring(1, tmpAmount.IndexOf("\"", 2) - 1);
                                    int.TryParse(tmpAmount, out operation.OperationAmount);
                                    tmpAmount = oneHourStat[0];
                                    tmpAmount = tmpAmount.Substring(tmpAmount.IndexOf("\"") + 1, tmpAmount.IndexOf(":")) + "00";
                                    string[] dataParts = tmpAmount.Split(new string[] { " " }, StringSplitOptions.None);
                                    if (dataParts[dataParts.Length - 1].Length == 4) dataParts[dataParts.Length - 1] = "0" + dataParts[dataParts.Length - 1];
                                    tmpAmount = string.Join(" ", dataParts);
                                    //operation.OperationDate = DateTime.Parse(tmpAmount, new CultureInfo("en-US", true));
                                    operation.OperationDate = DateTime.Parse(tmpAmount);
                                    if (operation.OperationDate.AddMonths(1) > DateTime.Now)
                                    {
                                        Operations.Add(operation);
                                    }
                                }
                                operation.OperationAmount = 0;
                                //...
                            }
                            double gunMaxPrice = Operations[0].OperationPrice;
                            double gunMinPrice = Operations[0].OperationPrice;
                            double gunAvgPrice = 0;
                            int gunSalesPcs = 0;
                            int daysCount = 1;
                            Operation prevOp = Operations[0];
                            foreach (var operation in Operations)
                            {
                                if (operation.OperationPrice > gunMaxPrice)
                                    gunMaxPrice = operation.OperationPrice;
                                if (operation.OperationPrice < gunMinPrice)
                                    gunMinPrice = operation.OperationPrice;

                                if (prevOp.OperationDate.Day != operation.OperationDate.Day)
                                {
                                    daysCount++;
                                    prevOp = operation;
                                }
                                gunSalesPcs += operation.OperationAmount;
                            }
                            gunAvgPrice = (gunMaxPrice + gunMinPrice) / 2;
                            gunSalesPcs = gunSalesPcs / daysCount;

                            DataRow myRow = dtDgv.NewRow();
                            string isName = sName.Text;
                            string link = result.GetAttribute("href");
                            string iiQty = qtyItem.Text;
                            myRow[0] = isName;
                            myRow[1] = link;
                            myRow[2] = iiQty;
                            myRow[3] = gunMaxPrice;
                            myRow[4] = gunMinPrice;
                            myRow[5] = gunAvgPrice;
                            myRow[6] = gunSalesPcs;

                            dtDgv.Rows.Add(myRow);
                            dtDgv.AcceptChanges();
                            
                            //source.ResetBindings(false);
                            

                        }
                        catch (Exception)
                        {

                            //throw;
                        }


                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                    e.Result = $"elem {rInd} of {results.Count}";
                    rInd++;
                }
                e.Result = $"{iPage} of {PAGECOUNT} scanned";

                System.Threading.Thread.Sleep(500);
                selector = By.Id("searchResults_btn_next");
                ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
                ww.Until(ExpectedConditions.ElementIsVisible(selector));
                browser.FindElement(selector).Click();

                sendingWorker.ReportProgress(iPage);
            }
        }
        protected void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled &&
                e.Error == null)//Check if the worker has been canceled or if an error occurred
            {

                dataGridView1.Update();
                dataGridView1.Refresh();

                lblStatus.Text = "Done";
            }
            else if (e.Cancelled)
            {
                lblStatus.Text = "User Canceled";
            }
            else
            {
                lblStatus.Text = "An error has occurred";
            }
            btnReadList.Enabled = true;//Re enable the start button
        }
        protected void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dataGridView1.Refresh();
            lblStatus.Text = string.Format("Counting number: {0}...", e.ProgressPercentage);
        }




        private void btnStartBrowser_Click(object sender, EventArgs e)
        {
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Navigate().GoToUrl(this.tbSteamInitialLink.Text);
            WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            ww.Until(ExpectedConditions.ElementIsVisible(By.ClassName("market_paging_pagelink")));
            List<IWebElement> Pages = browser.FindElements(By.ClassName("market_paging_pagelink")).ToList();
            int.TryParse(Pages.Last().Text, out PAGECOUNT);
            lblPagesCount.Text = PAGECOUNT.ToString();
            lblStatus.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selector = By.Id("searchResults_btn_next");
            /*WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            ww.Until(ExpectedConditions.ElementIsVisible(selector));*/

            WaitHelper.WaitUntil(browser, 10,
#pragma warning disable CS0618 // Тип или член устарел
                ExpectedConditions.ElementExists(selector),
#pragma warning restore CS0618 // Тип или член устарел
                "button X not found");
            browser.FindElement(selector).Click();
            //searchResults_btn_next
        }

        public struct Operation
        {
            public DateTime OperationDate;

            public Double OperationPrice;
            public int OperationAmount;
        }

        private void btnReadList_Click(object sender, EventArgs e)
        {
            //market_listing_row market_recent_listing_row market_listing_searchresult

            if (!myWorker.IsBusy)
            {
                lblStatus.Text = string.Empty;

                btnReadList.Enabled = false;
                myWorker.RunWorkerAsync(1);
            }


        }

        private void btnSaveXLS_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.FilterIndex = 1;
            sfd.DefaultExt = "xlsx";


            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = (DataTable)dtDgv;
                    wb.Worksheets.Add(dt, "ExportedData");
                    wb.SaveAs(sfd.FileName);
                    MessageBox.Show("Експорт завершений",
                                    "Експорт результатів",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }
    }
}
