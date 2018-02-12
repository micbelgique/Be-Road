using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.PublicServiceData
{
    public class MockPSDItunes : PublicServiceData
    {
        public string WrapperType { get; set; }
        public string Kind { get; set; }
        public string Artist { get; set; }
        public string Collection { get; set; }
        public string Track { get; set; }
        public string CollectionCensored { get; set; }
        public string TrackCensored { get; set; }
        public string ArtistViewURL { get; set; }
        public string CollectionViewURL { get; set; }
        public string TrackViewURL { get; set; }
        public string Preview { get; set; }
        public string ArtworkURL60 { get; set; }
        public string ArtworkURL100 { get; set; }
        public double CollectionPrice { get; set; }
        public double TrackPrice { get; set; }
        public string CollectionExplicitness { get; set; }
        public string TrackExplicitness { get; set; }
        public string DiscCount { get; set; }
        public string DiscNumber { get; set; }
        public string TrackCount { get; set; }
        public string TrackNumber { get; set; }
        public string TrackTimeMillis { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string PrimaryGenreName { get; set; }
    }
}