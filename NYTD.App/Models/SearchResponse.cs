using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Models
{
    public class SearchResponse
    {
        public List<Video> Videos { get; set; } = new List<Video>();
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

        public static SearchResponse operator+ (SearchResponse a, SearchResponse b)
        {
            a.Videos.AddRange(b.Videos);
            a.Channels.AddRange(b.Channels);
            a.Playlists.AddRange(b.Playlists);

            return a;
        }

        public void Clear()
        {
            Videos.Clear();
            Channels.Clear();
            Playlists.Clear();
        }

    }
}
