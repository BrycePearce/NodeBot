using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot.src.Models
{
    public class Director
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class TorrentSummary
    {
        public List<string> Sd { get; set; }
        public List<string> Hd { get; set; }
    }

    public class Torrent
    {
        public int TorrentId { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Size { get; set; }
        public string Snatched { get; set; }
        public string Seeders { get; set; }
        public string Leechers { get; set; }
    }

    public class GroupingQuality
    {
        public List<Torrent> Torrents { get; set; }
        public string QualityName { get; set; }
        public string CategoryName { get; set; }
    }

    public class Movie
    {
        public string GroupId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Cover { get; set; }
        public List<string> Tags { get; set; }
        public List<Director> Directors { get; set; }
        public string ImdbId { get; set; }
        public string ImdbRating { get; set; }
        public string ImdbVoteCount { get; set; }
        public int McRating { get; set; }
        public string McUrl { get; set; }
        public int RtRating { get; set; }
        public string RtUrl { get; set; }
        public int PtpRating { get; set; }
        public string PtpVoteCount { get; set; }
        public string YoutubeId { get; set; }
        public string Synopsis { get; set; }
        public TorrentSummary TorrentSummary { get; set; }
        public List<GroupingQuality> GroupingQualities { get; set; }
        public int RowSpan { get; set; }
        public string LatestTorrentTitle { get; set; }
        public string GroupTime { get; set; }
        public string MaxSize { get; set; }
        public string TotalSnatched { get; set; }
        public string TotalSeeders { get; set; }
        public string TotalLeechers { get; set; }
    }

    public class SomeMovieSiteObject
    {
        public List<Movie> Movies { get; set; }
        //public string AuthKey { get; set; }
        //public string TorrentPass { get; set; }
        public bool Grouping { get; set; }
        public bool ShowMovieNumber { get; set; }
    }
}
