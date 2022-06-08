using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaraokeSearcher.Dto
{
    public class SongList : ObservableCollection<Song> {}

    public class Song
    {
        public int room_id { get; set; }
        public string? title { get; set; }
        public string? artist { get; set; }
        public string? lyricist { get; set; }
        public string? composer { get; set; }
    }
}
