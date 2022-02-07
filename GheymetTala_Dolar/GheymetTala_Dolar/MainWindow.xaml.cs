using AngleSharp.Html.Dom;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GheymetTala_Dolar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HtmlDocument xhtml;

        /// <summary>
        /// تابع سازنده
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            xhtml = GetXHtmlFromUri("https://www.tgju.org/");

            GetPrices();
        }

        /// <summary>
        /// دریافت قیمت ها از طریق اسکرپ
        /// </summary>
        private void GetPrices()
        {
            Dolar_Price.Content = xhtml.GetElementbyId("l-bank_exchange_sell_usd").ChildNodes[3].InnerText;
            Euro_Price.Content = xhtml.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[6]/div/div[1]/div[2]/div/div[1]/table/tbody/tr[2]").ChildNodes[3].InnerText;
            Coin_Price.Content = xhtml.GetElementbyId("l-sekee").ChildNodes[3].InnerText;
            Gold_Price.Content = xhtml.GetElementbyId("l-geram18").ChildNodes[3].InnerText;
        }

        /// <summary>
        /// پرکردن فرم محاسبه و دریافت مقدار جواب
        /// </summary>
        private void SetAndGet()
        {
            xhtml.GetElementbyId("gold-calculator-weight").SetAttributeValue("value", txtGoldW.Text);
            xhtml.GetElementbyId("gold-calculator-pay").SetAttributeValue("value", txtMozd.Text);
            //HtmlElement fbLink = xhtml.GetElementbyId("gold-calculator-pay"); //webBrowser.Document.GetElementByID("fbLink");
           //.SetAttributeValue("onkeyup",)

            Gold_Price_Calc.Content = xhtml.GetElementbyId("gold-calculator-result").ChildNodes[0].InnerText;
        }

        private static HtmlDocument GetXHtmlFromUri(string uri)
        {
            WebClient client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            client.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

            HtmlDocument htmlDoc = new HtmlDocument()
            {
                OptionCheckSyntax = true,
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true,
                OptionDefaultStreamEncoding = Encoding.UTF8
            };

            htmlDoc.LoadHtml(client.DownloadString(uri));

            return htmlDoc;
        }

        /// <summary>
        /// کلیک تازه سازی
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetPrices();
        }

        /// <summary>
        /// دریافت قیمت محاسبه شده
        /// </summary>
        private void btnGoldCalc_Click(object sender, RoutedEventArgs e)
        {
            SetAndGet();
        }
    }
}
