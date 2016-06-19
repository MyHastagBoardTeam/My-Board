using System.Collections.Generic;
using System.Windows;

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
    }
}
