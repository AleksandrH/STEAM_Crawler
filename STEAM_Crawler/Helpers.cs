using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace STEAM_Crawler
{
    public static class WaitHelper
    {
        public static TResult WaitUntil<TResult>(IWebDriver webDriver,
                                                TimeSpan timeout,
                                                Func<IWebDriver, TResult> condition,
                                                string message = "")
        {
            var wait = new WebDriverWait(webDriver, timeout);
            if (!String.IsNullOrEmpty(message))
                wait.Message = message;

            return wait.Until(condition);
        }

        public static TResult WaitUntil<TResult>(IWebDriver webDriver,
                                                int timeoutSec,
                                                Func<IWebDriver, TResult> condition,
                                                string message = "")
        {
            return WaitUntil(webDriver, TimeSpan.FromSeconds(timeoutSec), condition, message);
        }
    }

    public class IntermediateParam
    {
        public static string ItemName;
        public static int CurrentPage;
        public IntermediateParam(string _ItemName, int _CurrentPage)
        {
            ItemName = _ItemName;
            CurrentPage = _CurrentPage;
        }
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

    public class SteamItem
    {
        [Description("Назва лота")]
        public string LargeIteminfoItemName;

        [Description("Ступінь поношеності")]
        public string LargeIteminfoItemDescriptor;

        [Description("Лінк на фото")]
        public string MarketListingLargeimage;

        [Description("Список операцій")]
        public List<Operation> ItemOperations;

        public SteamItem(string _largeIteminfoName, string _LargeIteminfoItemDescriptor, string _Image, List<Operation> _ItemOperations)
        {
            LargeIteminfoItemName = _largeIteminfoName;
            LargeIteminfoItemDescriptor = _LargeIteminfoItemDescriptor;
            MarketListingLargeimage = _Image;
            ItemOperations = _ItemOperations;
        }

        public SteamItem()
        {
        }
    }
}
