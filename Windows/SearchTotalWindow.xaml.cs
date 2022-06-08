using GaraokeSearcher.Dto;
using GaraokeSearcher.Libs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// SearchTotalWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SearchTotalWindow : Window
    {
        public string keyword;
        public SearchTotalWindow(string keyword)
        {
            InitializeComponent();
            this.keyword = keyword;

            reload();
        }

        async public void reload()
        {
            JObject result = await HttpApiClient.Call("songs/total-search?keyword=" + this.keyword);
            if (result != null && result["status"].Equals("success"))
            {
                MessageBox.Show("목록을 불러올 수 없습니다.");
                return;
            }
            List<Song> tjListById = new List<Song>();
            List<Song> tjListByTitle = new List<Song>();
            List<Song> kyListById = new List<Song>();
            List<Song> kyListByTitle = new List<Song>();
            JToken data = result["data"];
            JToken song_tj = data["song_tj"];
            foreach (JToken item in song_tj["by_title"])
            {
                tjListByTitle.Add(new Song()
                {
                    room_id = int.Parse(item["room_id"].ToString()),
                    title = item["title"].ToString(),
                    artist = item["artist"].ToString(),
                    lyricist = item["lyricist"].ToString(),
                    composer = item["composer"].ToString(),
                });
            }
            foreach (JToken item in song_tj["by_room_id"])
            {
                tjListById.Add(new Song()
                {
                    room_id = int.Parse(item["room_id"].ToString()),
                    title = item["title"].ToString(),
                    artist = item["artist"].ToString(),
                    lyricist = item["lyricist"].ToString(),
                    composer = item["composer"].ToString(),
                });
            }

            JToken song_ky = data["song_ky"];
            foreach (JToken item in song_ky["by_title"])
            {
                kyListByTitle.Add(new Song()
                {
                    room_id = int.Parse(item["room_id"].ToString()),
                    title = item["title"].ToString(),
                    artist = item["artist"].ToString(),
                    lyricist = item["lyricist"].ToString(),
                    composer = item["composer"].ToString(),
                });
            }
            foreach (JToken item in song_ky["by_room_id"])
            {
                kyListById.Add(new Song()
                {
                    room_id = int.Parse(item["room_id"].ToString()),
                    title = item["title"].ToString(),
                    artist = item["artist"].ToString(),
                    lyricist = item["lyricist"].ToString(),
                    composer = item["composer"].ToString(),
                });
            }

            tjListByIdControl.ItemsSource = tjListById;
            tjListByTitleControl.ItemsSource = tjListByTitle;
            kyListByIdControl.ItemsSource = kyListById;
            kyListByTitleControl.ItemsSource = kyListByTitle;
        }


        void searchHandler(object sender, EventArgs e)
        {
            this.keyword = searchTextBox.Text;
            reload();
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                searchHandler(sender, e);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void clickDetail(object sender, EventArgs e)
        {
            var label = sender as Label;
            var tagInfo = label?.Tag.ToString()?.Split(',');

            if (tagInfo != null && tagInfo.Length == 2)
            {
                string searchTarget = tagInfo[0].ToString();
                string searchType = tagInfo[1].ToString();

                var window = new SearchDetailWindow(this, searchTarget, searchType);
                window.Show();

                this.Hide();
            }
        }
    }
}
