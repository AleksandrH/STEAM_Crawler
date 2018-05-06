using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace STEAM_Crawler
{
    public partial class Form1 : Form
    {
        IWebDriver browser;
        public static int PAGECOUNT;
        public static DataTable dataTableForDataGrid;
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


            dataTableForDataGrid = new DataTable("dtdgv");

            dataTableForDataGrid.Columns.Add("ITEM_NAME", typeof(string));
            dataTableForDataGrid.Columns.Add("ITEM_LINK", typeof(string));
            dataTableForDataGrid.Columns.Add("ITEM_AT_MRKT", typeof(string));
            dataTableForDataGrid.Columns.Add("ITEM_MAX_PRICE", typeof(double));
            dataTableForDataGrid.Columns.Add("ITEM_MIN_PRICE", typeof(double));
            dataTableForDataGrid.Columns.Add("ITEM_AVG_PRICE", typeof(double));
            dataTableForDataGrid.Columns.Add("ITEM_AVG_SALES", typeof(double));
            dataTableForDataGrid.Columns.Add("ITEM_NAMEID", typeof(double));
            source.DataSource = dataTableForDataGrid;
            dataGridView1.DataSource = source;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        protected void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;
            //market_listing_row_link
            for (int iPage = 1; iPage <= PAGECOUNT; iPage++)
            {
                browser.Navigate().Refresh();
                var selector = By.ClassName("market_listing_row_link");
                WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(15));
                ww.Until(ExpectedConditions.ElementIsVisible(selector));

                List<IWebElement> results =
                        browser.FindElements(By.ClassName("market_listing_row_link")).ToList();
                int resultIndex = 1; //tmp var for result count
                foreach (IWebElement result in results)
                {

                    if (!sendingWorker.CancellationPending)
                    {
                        //try
                        //{
                        IWebElement item;
                        IWebElement itemName;
                        IWebElement sName;
                        IWebElement qtyItem;
                        string isName = string.Empty;
                        string iiQty = string.Empty;
                        string HTMLpath = result.GetAttribute("href").ToString();
                        string ITEM_NAMEID = string.Empty;
                        if (HTMLpath == string.Empty)
                        {
                            break;
                        }
                        for (int i = 0; i < 5; i++)
                        {

                            try
                            {
                                item = result.FindElement(By.ClassName("market_listing_searchresult"));
                                itemName = item.FindElement(By.ClassName("market_listing_item_name_block"));
                                sName = itemName.FindElement(By.ClassName("market_listing_item_name"));
                                qtyItem = result.FindElement(By.ClassName("market_listing_num_listings_qty"));

                                isName = sName.Text;
                                iiQty = qtyItem.Text;
                                break;
                            }
                            catch (Exception eX)
                            {
                                Debug.WriteLine(eX.Message);
                            }
                        }

                        List<Operation> Operations = new List<Operation>();
                        System.Threading.Thread.Sleep(20000); //to avoid microban
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HTMLpath);
                        using (Stream stream = request.GetResponse().GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string htmlCode = reader.ReadToEnd();

                                //Market_LoadOrderSpread(7178002);	// initial load
                                string strStartCriteria = "Market_LoadOrderSpread(";
                                string strEndCriteria = ");	// initial load";
                                int startPosition = htmlCode.IndexOf(strStartCriteria);
                                int endPosition = htmlCode.IndexOf(strEndCriteria, startPosition);
                                int substrLength = endPosition - (strStartCriteria.Length + startPosition);
                                string stat = htmlCode.Substring((startPosition + strStartCriteria.Length), substrLength);
                                ITEM_NAMEID = stat;


                                strStartCriteria = "var line1=[";
                                strEndCriteria = "g_timePriceHistoryEarliest";
                                startPosition = htmlCode.IndexOf(strStartCriteria);
                                endPosition = htmlCode.IndexOf(strEndCriteria, startPosition);
                                substrLength = endPosition - (strStartCriteria.Length + startPosition);
                                stat = htmlCode.Substring((startPosition + strStartCriteria.Length), substrLength);
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
                                    if (dataParts[dataParts.Length - 1].Length == 4)
                                    {
                                        dataParts[dataParts.Length - 1] = "0" + dataParts[dataParts.Length - 1];
                                    }
                                    tmpAmount = string.Join(" ", dataParts);
                                    operation.OperationDate = DateTime.Parse(tmpAmount);
                                    if (operation.OperationDate.AddMonths(1) > DateTime.Now)
                                    {
                                        Operations.Add(operation);
                                    }
                                }
                                operation.OperationAmount = 0;
                            }
                        }


                        double gunMaxPrice = 0;
                        double gunMinPrice = 0;
                        double gunAvgPrice = 0;
                        int gunSalesPcs = 0;
                        int daysCount = 1;
                        if (Operations.Count != 0)
                        {
                            gunMaxPrice = Operations[0].OperationPrice;
                            gunMinPrice = Operations[0].OperationPrice;
                            gunAvgPrice = 0;
                            gunSalesPcs = 0;
                            daysCount = 1;
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
                        }
                        gunAvgPrice = (gunMaxPrice + gunMinPrice) / 2;
                        gunSalesPcs = gunSalesPcs / daysCount;

                        DataRow myRow = dataTableForDataGrid.NewRow();
                        string link = result.GetAttribute("href");
                        myRow[0] = isName;
                        myRow[1] = link;
                        myRow[2] = iiQty;
                        myRow[3] = gunMaxPrice;
                        myRow[4] = gunMinPrice;
                        myRow[5] = gunAvgPrice;
                        myRow[6] = gunSalesPcs;
                        myRow[7] = Convert.ToDouble(ITEM_NAMEID);

                        dataTableForDataGrid.Rows.Add(myRow);
                        Debug.WriteLine(myRow[0]);
                        dataTableForDataGrid.AcceptChanges();


                        //}
                        //catch (Exception)
                        //{

                        //    //throw;
                        //}


                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                    resultIndex++;
                }
                System.Threading.Thread.Sleep(10000); //to avoid microban
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
                MessageBox.Show("Сканування завершено", "SteamCrawler", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dataTableForDataGrid.AcceptChanges();
            dataGridView1.Refresh();
            lblStatus.Text = string.Format("Counting number: {0}...", e.ProgressPercentage);
        }




        private void btnStartBrowser_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception)
            {

                browser.Close();
                Debug.WriteLine("Помилка відкриття браузера");
            }
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
                    DataTable dt = (DataTable)dataTableForDataGrid;
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
