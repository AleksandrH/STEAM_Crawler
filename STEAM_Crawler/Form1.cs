using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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
    public partial class frmMain : Form
    {
        IWebDriver browser;
        public static int PAGECOUNT;
        public static DataTable dataTableForDataGrid; // Грід з форми
        public static BindingSource source = new BindingSource();
        public BackgroundWorker myWorker = new BackgroundWorker();

        public frmMain()
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

            // Перебір всіх аркушів з результатами
            for (int iPage = 1; iPage <= PAGECOUNT; iPage++)
            {

                browser.Navigate().Refresh();
                
                // Один рядок результату - одна позиція
                var selector = By.ClassName("market_listing_row_link");

       
                WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(15));
                var element = ww.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(selector));

                // Пошук всіх позицій із поточної сторінки
                List<IWebElement> results =
                        browser.FindElements(By.ClassName("market_listing_row_link")).ToList();
                
                
                int resultIndex = 1; //tmp var for result count


                // перебор знайдених позицій
                foreach (IWebElement result in results)
                {
                    // Зупинки виконання не було
                    if (!sendingWorker.CancellationPending)
                    {
                        //try
                        //{
                        IWebElement item = null;
                        IWebElement itemName = null;
                        IWebElement sName = null;
                        IWebElement qtyItem = null;
                        string isName = string.Empty;
                        string iiQty = string.Empty;
                        string HTMLpath = result.GetAttribute("href").ToString();
                        string ITEM_NAMEID = string.Empty;

                        // позиція без сторінки деталей - зупинити дальшу обробку цієї позиції
                        if (HTMLpath == string.Empty)
                        {
                            break;
                        }



                        // для чого 5 раз шукати те саме?
                        // незрозумілий хід думки
                        for (int i = 0; i < 5; i++)
                        {

                            try
                            {
                                // позиція
                                item = item ?? result?.FindElement(By.ClassName("market_listing_searchresult"));

                                // заголовок (назва+гра)

                                itemName = itemName ?? item?.FindElement(By.ClassName("market_listing_item_name_block"));
                                
                                // назва товару
                                sName = sName ?? itemName?.FindElement(By.ClassName("market_listing_item_name"));
                                isName = sName.Text;

                                // лотів на торговій площадці
                                qtyItem = qtyItem ?? result?.FindElement(By.ClassName("market_listing_num_listings_qty"));
                                iiQty = qtyItem.Text;
                                
                                break;
                            }
                            catch (Exception eX)
                            {
                                Debug.WriteLine(eX.Message);
                            }
                        }


                        // операції з позицією
                        List<Operation> Operations = new List<Operation>();

                        // чекати 15с 
                        System.Threading.Thread.Sleep(15000); //to avoid microban

                        // прочитати сторінку детальними даними
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HTMLpath);
                        using (Stream stream = request.GetResponse().GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string htmlCode = reader.ReadToEnd();

                                #region ID товару
                                // ID товару знаходиться між тегами
                                // "Market_LoadOrderSpread("	
                                // та 
                                // "// initial load"
                                string strStartCriteria = "Market_LoadOrderSpread(";
                                string strEndCriteria = ");	// initial load";

                                // початок статистичних даних
                                int startPosition = htmlCode.IndexOf(strStartCriteria);
                                
                                // кінець статистичних даних
                                int endPosition = htmlCode.IndexOf(strEndCriteria, startPosition);
                                
                                // статистичні дані
                                int substrLength = endPosition - (strStartCriteria.Length + startPosition);

                                // ID товару
                                ITEM_NAMEID = htmlCode.Substring((startPosition + strStartCriteria.Length), substrLength);

                                #endregion

                                #region Здійснені операції за весь час, дата, кількість
                                // алгоритм не універсальний. Не завжди є вказані критерії в коді сторінки

                                // статистика знаходиться між тегами
                                // "var line1=[" 
                                strStartCriteria = "var line1=[";
                                // та "g_timePriceHistoryEarliest"
                                strEndCriteria = "g_timePriceHistoryEarliest";
                                // у вигляді ["Mmm DD YYYY hh: +0",8.091,"4"]
                                // Mmm DD YYYY - Дата
                                // hh - години 01..24
                                // +0 - n/a
                                // 8.091 - ціна продажу
                                // "4" - кількість проданих одиниць
                               
                                startPosition = htmlCode.IndexOf(strStartCriteria);
                                endPosition = htmlCode.IndexOf(strEndCriteria, startPosition);
                                substrLength = endPosition - (strStartCriteria.Length + startPosition);

                                string stat = htmlCode.Substring((startPosition + strStartCriteria.Length), substrLength);

                                // "уніфікація" тексту
                                stat = stat.Replace("\t", "").Replace("\n", "");

                                // розбиття по статистичних одиницях
                                string[] cells = stat.Split(new string[] { "],[" }, StringSplitOptions.None);

                                // статистика в порядку спадання дат
                                Array.Reverse(cells);
                                #endregion

                                // потрібна статистика за останніх 30 днів
                                // 30 днів, 24 години/день 
                                // 30 * 24 - 30 днів по 24 години
                                // 720 записів за умови щогодинного продажу

                                // Як виділити нульові продажі?

                                // Розмір масиву для збереження інформації - МАХ(прочитана_статистика, 30*24)
                                int newArraySize = 30 * 24 > cells.Length ? cells.Length : 30 * 24;


                                string[] lastMonthStat = new string[newArraySize];
                                Array.Copy(cells, 0, lastMonthStat, 0, newArraySize);

                                // годинна статистика операцій
                                Operation operation = new Operation();
                                
                                foreach (string hourStat in lastMonthStat)
                                {
                                    //розбити один запис на складові
                                    string[] oneHourStat = hourStat.Split(',');

                                    // ціна продажу - 2й елемент
                                    Double.TryParse(oneHourStat[1].Replace('.', ','), out operation.OperationPrice);

                                    // кількість одиниць товару
                                    string tmpAmount = oneHourStat[2];
                                    tmpAmount = tmpAmount.Substring(1, tmpAmount.IndexOf("\"", 2) - 1);
                                    int.TryParse(tmpAmount, out operation.OperationAmount);


                                    // підготувати дату до переведення в date
                                    // "Mmm DD YYYY hh: +0" => "Mmm DD YYYY hh:00"
                                    tmpAmount = oneHourStat[0];
                                    tmpAmount = tmpAmount.Substring(tmpAmount.IndexOf("\"") + 1, tmpAmount.IndexOf(":")) + "00";
                                    
                                    string[] dataParts = tmpAmount.Split(new string[] { " " }, StringSplitOptions.None);
                                    
                                    // якщо годин менше 10, то додати початковий 0
                                    if (dataParts[dataParts.Length - 1].Length == 4)
                                    {
                                        dataParts[dataParts.Length - 1] = "0" + dataParts[dataParts.Length - 1];
                                    }
                                    // звести дату операції в одне ціле
                                    tmpAmount = string.Join(" ", dataParts);
                                    operation.OperationDate = DateTime.Parse(tmpAmount);

                                    // потрібна статистика лише останнього місяця
                                    // якщо мало продажу було, то записів може бути не 720,
                                    // старші за місяць записи не відображають продаж погодинно
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
                                // найвища ціна продажу за період
                                if (operation.OperationPrice > gunMaxPrice)
                                    gunMaxPrice = operation.OperationPrice;

                                // найнижча ціна продажу за період
                                if (operation.OperationPrice < gunMinPrice)
                                    gunMinPrice = operation.OperationPrice;


                                // кількість днів статистики
                                if (prevOp.OperationDate.Day != operation.OperationDate.Day)
                                {
                                    daysCount++;
                                    prevOp = operation;
                                }

                                // оборот товару за період
                                gunSalesPcs += operation.OperationAmount;
                            }
                        }

                        // середня ціна за період
                        gunAvgPrice = (gunMaxPrice + gunMinPrice) / 2;

                        // передньоденний продаж, шт
                        gunSalesPcs /= daysCount;

                        // вивід інформації на грід
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

                System.Threading.Thread.Sleep(3000); //to avoid microban

                // емуляція переходу на наступний аркуш з результатами
                selector = By.Id("searchResults_btn_next");
                ww = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
                ww.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(selector));
                browser.FindElement(selector).Click();

                // номер опрацьованої сторінки відбразити в статус-барі. 
                // не відображається
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
                lblStatus.Text = "An error has occurred" + e.Error.Message;
            }
            btnReadList.Enabled = true;//Re enable the start button
        }


        /// <summary>
        /// зберегти проміжні результати гріда
        /// відобразити дані в статус-барі
        /// </summary>
        protected void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Не працює :(

            dataTableForDataGrid.AcceptChanges();
            dataGridView1.Refresh();
            IntermediateSaving("D:\\FileName_LIST.xlsx");
            lblStatus.Text = string.Format("Counting number: {0}...", e.ProgressPercentage);
        }




        /// <summary>
        /// Запуск браузера з потрібної сторінки
        /// </summary>
        private void btnStartBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                // Відкрити потрібну сторінку
                browser = new OpenQA.Selenium.Chrome.ChromeDriver();
                browser.Navigate().GoToUrl(this.tbSteamInitialLink.Text);
                WebDriverWait ww = new WebDriverWait(browser, TimeSpan.FromSeconds(20));

                // шукати елементи-ідентифікатори
                try
                {
                    // 1 сторінка результатів
                    ww.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("market_paging_summary ellipsis")));
                    PAGECOUNT = 1;
                }
                catch
                {
                    // Результатів більше 1 сторінки
                    ww.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("market_paging_pagelink")));
                    List<IWebElement> Pages = browser.FindElements(By.ClassName("market_paging_pagelink")).ToList();
                    int.TryParse(Pages.Last().Text, out PAGECOUNT);
                }

                // Кількість знайдених сторінок з лотами
                lblPagesCount.Text = PAGECOUNT.ToString();
                lblStatus.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // Помилка браузера
                browser.Close();
                MessageBox.Show("Помилка відкриття браузера\n" + ex.Message,
                                "Ініціалізація браузера",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine("Помилка відкриття браузера\n"+ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Quit();
        }






        /// <summary>
        /// Статистика годинного продажу
        /// </summary>
        public struct Operation
        {
            // Дата операції
            public DateTime OperationDate;

            // Ціна
            public Double OperationPrice;

            // Кількість
            public int OperationAmount;
        }


        /// <summary>
        /// Запуск збору інформації
        /// </summary>
        private void btnReadList_Click(object sender, EventArgs e)
        {
            // Критерії пошуку результатів на сторінці
            // хз для чого то тут написано
            // market_listing_row market_recent_listing_row market_listing_searchresult

            if (!myWorker.IsBusy)
            {
                // Статус-бар
                lblStatus.Text = string.Empty;
                btnReadList.Enabled = false;

                myWorker.RunWorkerAsync(1);
            }


        }


        /// <summary>
        /// Проміжне збереження результатів
        /// після кожної опрацьованої сторінки
        /// щоб не факапнутися якщо основна програма вилетить
        /// </summary>
        /// <param name="FileName">назва файла результатів</param>
        private void IntermediateSaving(string FileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = (DataTable)dataTableForDataGrid;
                wb.Worksheets.Add(dt, "ExportedData");
                wb.SaveAs(FileName);
            }
       
        }

        /// <summary>
        /// Збереження результатів пошуку
        /// </summary>
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
