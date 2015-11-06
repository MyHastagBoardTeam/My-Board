using System;
using System.Collections.Generic;
using System.Linq;
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
            
        }
        private void KeyDown_Press(object sender, KeyEventArgs e)
        {

        }

        private void Category_Changed(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
