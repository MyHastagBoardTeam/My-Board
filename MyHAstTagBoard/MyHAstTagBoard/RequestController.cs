using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{
    

    public class RequestController
    {
        Func<string, List<TextBlock>> tbL = null;
        object locker = new object();
        private ITEvent currentEvent = null;
        private Media attachedMedia = null;
        private MainWindow window = null;

        public RequestController(MainWindow win)
        {
            window = win;
            currentEvent = new ITEvent();
            attachedMedia = new Media();
            window.InfoButton.Click += InfoButton_Click;
            window.SourceButton.Click += SourceButton_Click;
            window.PurchaseButton.Click += PurchaseButton_Click;
            window.CategoriesBox.ItemsSource = currentEvent.categories;
            Func<string, List<TextBlock>> tbL = ParseRSS;
        }

        private void PurchaseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SourceButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InfoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Shows the search result in a TextBlock async 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void ChangeCategory(object sender, SelectionChangedEventArgs e)
        //{
        //    var me = sender as System.Windows.Controls.ComboBox;

        //    Task<List<TextBlock>>.Factory.StartNew((i) =>
        //    {
        //        return ParseRSS(me.SelectedValue.ToString());
        //    }, me.SelectedValue.ToString())

        //    .ContinueWith((previousTask) =>
        //    {
        //        var result = previousTask.Result;
        //        window.RequestedEvents.Dispatcher.Invoke(() =>
        //        { 
        //            window.RequestedEvents.ItemsSource = result;
        //            ICollectionView view = CollectionViewSource.GetDefaultView(window.RequestedEvents.ItemsSource);
        //            view.Refresh();
        //        });
        //    });
        //    System.Windows.Forms.MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
        //}
        /// <summary>
        /// Parses rss
        /// </summary>
        /// <param name="rss">object rss</param>
        public List<TextBlock> ParseRSS(string rss)
        {
            List<TextBlock> Events = new List<TextBlock>();
            lock (locker)
            {
                string rssUrl = (string)rss;
                XmlDocument rssXmlDoc = new XmlDocument();
                rssXmlDoc.Load(rssUrl);
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
                StringBuilder rssContent = new StringBuilder();

                foreach (XmlNode node in rssNodes)
                {

                    XmlNode rssSubNode = node.SelectSingleNode("title");
                    currentEvent.Title = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = node.SelectSingleNode("link");
                    currentEvent.Source.Add(new Uri(rssSubNode != null ? rssSubNode.InnerText : ""));

                    rssSubNode = node.SelectSingleNode("description");
                    currentEvent.Content = rssSubNode != null ? rssSubNode.InnerText : "";

                    if (rssSubNode != null)
                    {
                        currentEvent.Content = Regex.Replace(rssSubNode.InnerText, @"<[^>]+>|&nbsp;", "").Trim();//Remove html
                        currentEvent.Content = currentEvent.Content.Replace("\n\n", "\n"); // Remove space lines
                        rssContent.Append("<a href='" + currentEvent.Source + "'>" + currentEvent.Title + "</a><br>\n" + currentEvent.Content);
                    }
                    rssContent.Append("<a href='" + currentEvent.Source + "'>" + currentEvent.Title + "</a><br>\n");
                    TextBlock tb = new TextBlock();
                    tb.LineHeight = 0.8d;
                    tb.Background = Brushes.AntiqueWhite;
                    tb.FontSize = 10;
                    tb.Foreground = Brushes.DarkBlue;
                    tb.Text = currentEvent.Content;
                    Events.Add(tb);
                }
                
            }
            return Events;
        }
    }
}