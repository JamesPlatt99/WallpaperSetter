using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using System.Linq;

namespace Wallpaper_Picker
{
    class ImageRetriever
    {
        #region enumeration
        public enum SubredditSource
        {
            random = 999,
            earthPorn = 0,
            spacePorn = 1,
            wallpapers = 2,
            villagePorn = 3
        }

        #endregion

        #region properties

        private int _selectedSource;
        public string _selectedSourceURL
        {
            get
            {
                if (_selectedSource == (int)SubredditSource.random) 
                {
                    Random rnd = new Random();
                    _selectedSource = rnd.Next(0, _sources.Count());
                }
                return Sources[_selectedSource]; 
            }
        }
        private List<String> _sources;
        private List<String> Sources{
            get{
                if(_sources == null)
                {
                    _sources = new List<String>()
                            {"https://www.reddit.com/r/EarthPorn/",
                             "https://www.reddit.com/r/spaceporn/",
                             "https://www.reddit.com/r/wallpapers/",
                             "https://www.reddit.com/r/VillagePorn/"
                            }.ToList();
                }
                return _sources ;
            }
        }
        #endregion

        #region constructor

        public ImageRetriever(SubredditSource selectedSource = SubredditSource.random)
        {
            _selectedSource = (int)selectedSource;
        }

        #endregion

        #region public methods

        public string GetNewWallpaperURL()
        {
            var results = GetJSONResults();
            if(results.Any()){
                var rnd = new Random();
                int selectedIndex = rnd.Next(0, results.Count() - 1);
                return results[selectedIndex];
            }
            return null;
        }

        #endregion

        #region helper methods
        private List<string> GetJSONResults()
        {
            var client = new RestClient(_selectedSourceURL);
            var request = new RestRequest(".json", Method.GET);
            var response = client.Execute(request);
            var posts = JsonConvert.DeserializeObject<RootObject>(response.Content).data.children
                                .Select(n=> n.data.preview.images[0].source)
                                .Where(n=> ValidateImage(n));
            return posts.Select(n => n.url).ToList();
        }

        private Boolean ValidateImage(Source image)
        {
            return image.width >= 1920 && image.height >= 1080;
        }
        #endregion

    }
}
