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
            random = 0,
            earthPorn = 1,
            spacePorn = 2,
            wallpapers = 3,
            villagePorn = 4
        }

        #endregion

        #region properties

        private int _selectedSource;
        public string _selectedSourceURL
        {
            get
            {
                switch (_selectedSource)
                {
                    case 1:
                        return "https://www.reddit.com/r/EarthPorn/";
                    case 2:
                        return "https://www.reddit.com/r/spaceporn/";
                    case 3:
                        return "https://www.reddit.com/r/wallpapers/";
                    case 4:
                        return "https://www.reddit.com/r/VillagePorn/";
                    default:
                        Random rnd = new Random();
                        _selectedSource = rnd.Next(1, 4);
                        return _selectedSourceURL;
                }
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
