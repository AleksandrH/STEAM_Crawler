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
using OpenQA.Selenium;

namespace STEAM_Crawler
{
    public partial class Form1 : Form
    {
        IWebDriver browser;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("itemName", "ITEM_NAME");
            dataGridView1.Columns.Add("itemLink", "ITEM_LINK");
            dataGridView1.Columns.Add("itemMaxPrice", "ITEM_MAX_PRICE");
            dataGridView1.Columns.Add("itemMinPrice", "ITEM_MIN_PRICE");
            dataGridView1.Columns.Add("itemAvgPrice", "ITEM_AVG_PRICE");
            dataGridView1.Columns.Add("itemAvgSales", "ITEM_AVG_SALES");



            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnGetStat_Click(object sender, EventArgs e)
        {
            browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            browser.Navigate().GoToUrl(this.tbSteamInitialLink.Text);

            List<IWebElement> Pages = browser.FindElements(By.ClassName("market_paging_pagelink")).ToList();
            lblPagesCount.Text = Pages.Last().Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            browser.FindElement(By.Id("searchResults_btn_next")).Click();
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
            List<IWebElement> results = browser.FindElements(By.ClassName("market_listing_row_link")).ToList();
            foreach (IWebElement result in results)
            {


                var item = result.FindElement(By.ClassName("market_listing_searchresult"));
                var itemName = item.FindElement(By.ClassName("market_listing_item_name_block"));
                var sName = itemName.FindElement(By.ClassName("market_listing_item_name"));
                try
                {
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
                            tmpAmount = tmpAmount.Substring(1, tmpAmount.IndexOf(":"));
                            operation.OperationDate = DateTime.Parse(tmpAmount + "00");
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
                    this.dataGridView1.Rows.Add(sName.Text, result.GetAttribute("href"), gunMaxPrice, gunMinPrice, gunAvgPrice, gunSalesPcs);
                }
                catch (Exception ex)
                {

                    this.dataGridView1.Rows.Add(sName.Text, result.GetAttribute("href"), ex.Message);
                }
            }

        }
    }
}
