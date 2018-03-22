using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace STEAM_Crawler
{
    public partial class Form1 : Form
    {
        IWebDriver browser;
        public static int PAGECOUNT;
        public static DataTable dtDgv;
        public Form1()
        {
            InitializeComponent();
            dtDgv = new DataTable("dtdgv");

            dtDgv.Columns.Add("ITEM_NAME",typeof(string));
            dtDgv.Columns.Add( "ITEM_LINK", typeof(string));
            dtDgv.Columns.Add( "ITEM_AT_MRKT", typeof(string));
            dtDgv.Columns.Add( "ITEM_MAX_PRICE", typeof(double));
            dtDgv.Columns.Add( "ITEM_MIN_PRICE", typeof(double));
            dtDgv.Columns.Add( "ITEM_AVG_PRICE", typeof(double));
            dtDgv.Columns.Add("ITEM_AVG_SALES", typeof(double));
            dataGridView1.DataSource = dtDgv;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnGetStat_Click(object sender, EventArgs e)
        {
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Navigate().GoToUrl(this.tbSteamInitialLink.Text);
            WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            ww.Until(ExpectedConditions.ElementIsVisible(By.ClassName("market_paging_pagelink")));
            List<IWebElement> Pages = browser.FindElements(By.ClassName("market_paging_pagelink")).ToList();
            int.TryParse(Pages.Last().Text, out PAGECOUNT);
            lblPagesCount.Text = PAGECOUNT.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selector = By.Id("searchResults_btn_next");
            WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            ww.Until(ExpectedConditions.ElementIsVisible(selector));
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

            //market_listing_row_link
            for (int iPage = 1; iPage < PAGECOUNT; iPage++)
            {

                this.lblPagesCount.Text = $@"Page [{iPage}] of [{PAGECOUNT}]";

                var selector = By.ClassName("market_listing_row_link");
                WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
                ww.Until(ExpectedConditions.ElementIsVisible(selector));

                List<IWebElement> results = browser.FindElements(By.ClassName("market_listing_row_link")).ToList();
                foreach (IWebElement result in results)
                {

                    //try
                    //{
                        var item = result.FindElement(By.ClassName("market_listing_searchresult"));
                        var itemName = item.FindElement(By.ClassName("market_listing_item_name_block"));
                        var sName = itemName.FindElement(By.ClassName("market_listing_item_name"));
                        var qtyItem = result.FindElement(By.ClassName("market_listing_num_listings_qty"));
                    
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
                                tmpAmount = tmpAmount.Substring(tmpAmount.IndexOf("\"")+1, tmpAmount.IndexOf(":"))+"00";
                                string[] dataParts = tmpAmount.Split(new string[] { " " }, StringSplitOptions.None);
                                if (dataParts[dataParts.Length-1].Length == 4) dataParts[dataParts.Length-1] = "0" + dataParts[dataParts.Length-1];
                            tmpAmount = string.Join(" ", dataParts);
                            //operation.OperationDate = DateTime.Parse(tmpAmount, new CultureInfo("en-US", true));
                            operation.OperationDate = DateTime.Parse( tmpAmount); 
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

                    /*
                     * dtDgv.Columns.Add("ITEM_NAME",typeof(string));
        dtDgv.Columns.Add( "ITEM_LINK", typeof(string));
        dtDgv.Columns.Add( "ITEM_AT_MRKT", typeof(int));
        dtDgv.Columns.Add( "ITEM_MAX_PRICE", typeof(double));
        dtDgv.Columns.Add( "ITEM_MIN_PRICE", typeof(double));
        dtDgv.Columns.Add( "ITEM_AVG_PRICE", typeof(double));
        dtDgv.Columns.Add("ITEM_AVG_SALES", typeof(double));
                     * */
                    DataRow myRow = dtDgv.NewRow();
                    myRow[0] = sName.Text;
                    myRow[1] = result.GetAttribute("href");
                    myRow[2] = qtyItem.Text;
                    myRow[3] = gunMaxPrice;
                    myRow[4] = gunMinPrice;
                    myRow[5] = gunAvgPrice;
                    myRow[6] = gunSalesPcs;

                    dtDgv.Rows.Add(myRow);
                    dtDgv.AcceptChanges();
                    
                    //this.dataGridView1.Rows.Add(sName.Text, result.GetAttribute("href"), qtyItem.Text, gunMaxPrice, gunMinPrice, gunAvgPrice, gunSalesPcs);
                    //}
                    //catch (Exception ex)
                    //{
                      //  MessageBox.Show(ex.Message);
                       // this.dataGridView1.Rows.Add(sName.Text, result.GetAttribute("href"), ex.Message);
                    //}

                    
                }
                System.Threading.Thread.Sleep(60000);
                selector = By.Id("searchResults_btn_next");
                ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
                ww.Until(ExpectedConditions.ElementIsVisible(selector));
                browser.FindElement(selector).Click();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.FilterIndex = 1;
            sfd.DefaultExt = "xlsx";
            

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
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
