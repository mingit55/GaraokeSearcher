using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Net.Http.Headers;
using GaraokeSearcher.Libs;
using GaraokeSearcher.Dto;
using GaraokeSearcher.Windows;

namespace GaraokeSearcher {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }


        void searchHandler(object sender, EventArgs e)
        {
            string keyword = searchTextBox.Text;
            SearchTotalWindow window = new SearchTotalWindow(keyword);
            window.Show();

            this.Close();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                searchHandler(sender, e);
            }
        }
    }
}
