using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyHAstTagBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> data = null;
        RequestController requests = null;
    
        public MainWindow()
        {
            InitializeComponent();
            requests = new RequestController(this);
            System.Windows.Forms.MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            CategoriesBox.SelectionChanged += ChangeCategory;

        }

        public void ChangeCategory(object sender, SelectionChangedEventArgs e)
        {
            var me = sender as System.Windows.Controls.ComboBox;

            //Task<List<TextBlock>>.Factory.StartNew((i) =>
            //{
            //    status.Content = "Loading...";
            //    return requests.ParseRSS(me.SelectedValue.ToString());
            //}, me.SelectedValue.ToString())
            status.Content = "Loading...";
            Task.Run(() =>
            {
                System.Windows.Forms.MessageBox.Show("Time to Parse RSS");
                return requests.ParseRSS(me.SelectedValue.ToString());
            })
            .ContinueWith((prevTask) =>
            {
                System.Windows.Forms.MessageBox.Show("Hi from ContinueWith");
                System.Windows.Forms.MessageBox.Show(Dispatcher.CheckAccess().ToString());
                var result = prevTask.Result;
                Dispatcher.Invoke(() =>
                {
                    System.Windows.Forms.MessageBox.Show("Hi from DISPATCHER (WORKED \\o/)");
                    status.Content = "Loaded";
                    RequestedEvents.ItemsSource = result;
                    ICollectionView view = CollectionViewSource.GetDefaultView(this.RequestedEvents.ItemsSource);
                    view.Refresh();
                });
            });
            //System.Windows.Forms.MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
        }
        private void Category_Changed(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
