using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{
    public class RequestController
    {
        private ITEvent currentEvent = null;
        private Media attachedMedia = null;
        private MainWindow window = null;

        public RequestController(MainWindow win)
        {
            window = win;
            currentEvent = new ITEvent();
            attachedMedia = new Media();
            window.CategoriesBox.SelectionChanged += ChangeCategory;
            window.InfoButton.Click += InfoButton_Click;
            window.SourceButton.Click += SourceButton_Click;
            window.PurchaseButton.Click += PurchaseButton_Click;
            window.CategoriesBox.ItemsSource = currentEvent.categories;
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

        public void ChangeCategory(object sender, SelectionChangedEventArgs e)
        {
            ComboBox me = (ComboBox)sender;
            ParseRSS(me.SelectedValue.ToString());
        }

        public void ParseRSS(string rssUrl)
        {

            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(rssUrl);

            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();

            List<Label> l = new List<Label>();

            foreach (XmlNode node in rssNodes)
            {
                XmlNode rssSubNode = node.SelectSingleNode("title");
                currentEvent.Title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = node.SelectSingleNode("link");
                currentEvent.Source.Add(new Uri(rssSubNode != null ? rssSubNode.InnerText : ""));

                rssSubNode = node.SelectSingleNode("description");

                if (rssSubNode != null)
                {
                    currentEvent.Content = Regex.Replace(rssSubNode.InnerText, @"<[^>]+>|&nbsp;", "").Trim();
                    rssContent.Append("<a href='" + currentEvent.Source + "'>" + currentEvent.Title + "</a><br>\n" + currentEvent.Content);
                }
                rssContent.Append("<a href='" + currentEvent.Source + "'>" + currentEvent.Title + "</a><br>\n");
                Label ll = new Label();
                ll.Content = currentEvent.Content;
                l.Add(ll);
            }
            window.RequestedEvents.ItemsSource = l;
        }
    }
}
