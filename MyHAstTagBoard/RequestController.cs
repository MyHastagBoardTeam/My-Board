using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace MyHAstTagBoard
{

    public class RequestController
    {
        object locker = new object();
        private ITEventsList currentEvent = null;
        private MediaContent attachedMedia = null;
        private MainWindow window = null;
        private EventInfoWindow _eventWindow;
        private EventPageController _eventPageController;
        private XmlNodeList _rssXmlEventsList;
        private List<string> _events;

        public RequestController(MainWindow win)
        {
            window = win;
            currentEvent = new ITEventsList();
            attachedMedia = new MediaContent();
            window.CategoriesBox.SelectionChanged += ChangeCategory;
            window.InfoButton.Click += InfoButton_Click;
            window.SourceButton.Click += SourceButton_Click;
            window.PurchaseButton.Click += PurchaseButton_Click;
            window.RequestedEvents.SelectionChanged += OnSelectedEvent;
            window.CategoriesBox.ItemsSource = currentEvent.categories;
            _eventWindow = new EventInfoWindow();
            Func<string, List<string>> parsingRssdelegate = ParseRSS;
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

           // _eventPageController = new EventPageController();
            _eventWindow.Show();
        }
        private void OnSelectedEvent(object sender, SelectionChangedEventArgs e)
        {
            var ev = sender as ListView;
            int counter = ev.SelectedIndex;
            var node = _rssXmlEventsList.Item(counter);
            _eventWindow = new EventInfoWindow();
            _eventPageController = new EventPageController(_eventWindow, GetEventUri(node));
            _eventWindow.Show();  
        }
        /// <summary>
        /// Get pgae url for rendering at the UI
        /// </summary>
        /// <param name="rssNode">XmlNode thatshould be parsed for searching a page link</param>
        /// <returns>return an Uri instance, that contains HTML page URL</returns>
        private Uri GetEventUri(XmlNode rssNode)
        {
            var uri = rssNode.FirstChild.BaseURI;
            string temp = null;
            string ur = rssNode.InnerText;
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(rssNode.BaseURI);
            _rssXmlEventsList = rssXmlDoc.SelectNodes("rss/channel/item/link");
            foreach (XmlNode node in _rssXmlEventsList)
            {
                temp = node.InnerText;
            }
            Uri returningUri = new Uri(temp);
            return returningUri;
        }
        /// <summary>
        /// Shows the search result of parsing from URLs in a TextBlock async 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeCategory(object sender, SelectionChangedEventArgs e)
        {
            var me = sender as System.Windows.Controls.ComboBox;
            string url = me.SelectedValue.ToString();
            SynchronizationContext syncMainTool = SynchronizationContext.Current;

            Task<List<string>>.Factory.StartNew((i) =>
            {
                return ParseRSS(url);
            }, null)

            .ContinueWith((previousTask) =>
            {
                var result = previousTask.Result;
                syncMainTool.Post(empty => window.RequestedEvents.ItemsSource = result, null);
            });
        }
        /// <summary>
        /// Parses rss
        /// </summary>
        /// <param name="rss">object rss</param>
        public List<string> ParseRSS(string rss)
        {
            _events = new List<string>();
            lock (locker)
            {
                string rssUrl = (string)rss;
                XmlDocument rssXmlDoc = new XmlDocument();
                rssXmlDoc.Load(rssUrl);
                _rssXmlEventsList = rssXmlDoc.SelectNodes("rss/channel/item");
                StringBuilder rssContent = new StringBuilder();

                foreach (XmlNode node in _rssXmlEventsList)
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
                    _events.Add(currentEvent.Title + currentEvent.Content);
                }

            }
            return _events;
        }
    }
}
