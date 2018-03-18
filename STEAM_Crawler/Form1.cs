using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            dataGridView1.Columns.Add( "itemName","ITEM_NAME");
            dataGridView1.Columns.Add("itemLink", "ITEM_LINK");
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
                this.dataGridView1.Rows.Add(sName.Text, result.GetAttribute("href"));

                using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
                {
                 
                    string htmlCode = client.DownloadString(result.GetAttribute("href"));
                    string strStartCriteria = "var line1=[";
                    string strEndCriteria = "g_timePriceHistoryEarliest";
                    int startPosition = htmlCode.IndexOf(strStartCriteria);
                    int endPosition = htmlCode.IndexOf(strEndCriteria, startPosition);
                    int substrLength = endPosition - (strStartCriteria.Length + startPosition);
                    string stat = htmlCode.Substring((startPosition + strStartCriteria.Length), substrLength);
                    string[] cells = stat.Split(new string[] { "],[" }, StringSplitOptions.None);
                    //...
                }
            }
        }
    }
}
