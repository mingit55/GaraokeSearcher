using GaraokeSearcher.Dto;
using GaraokeSearcher.Libs;
using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace GaraokeSearcher.Windows
{
    /// <summary>
    /// SearchDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchDetailWindow : Window
    {
        SearchTotalWindow prevWindow;
        string searchTarget;
        string searchType;
        public SearchDetailWindow(SearchTotalWindow window, string searchTarget, string searchType)
        {
            InitializeComponent();

            prevWindow = window;
            this.searchTarget = searchTarget;
            this.searchType = searchType;

            update();
        }

        async void update()
        {
            try
            {
                var res = await HttpApiClient.Call($"songs/{searchTarget}?keyword={prevWindow.keyword}&searchType={searchType}");
                if (res == null)
                {
                    return;
                }

                var status = res["status"].ToString();
                JToken data = res["data"];

                if (status != "success")
                {
                    return;
                }

                JToken jsonList = data["list"];
                List<Song> songList = new List<Song>();
                foreach (JToken jsonItem in jsonList)
                {
                    songList.Add(new Song()
                    {
                        room_id = int.Parse(jsonItem["room_id"].ToString()),
                        title = jsonItem["title"].ToString(),
                        artist = jsonItem["artist"].ToString(),
                        composer = jsonItem["composer"].ToString(),
                        lyricist = jsonItem["lyricist"].ToString(),
                    });
                }

                listViewControl.ItemsSource = songList;
            }
            catch
            {
                MessageBox.Show("목록을 불러오지 못했습니다.");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.prevWindow.Show();
            this.Close();
        }
    }
}
