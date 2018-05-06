using System;
using System.Collections.Generic;
using System.Text;

namespace Wallpaper_Picker
{
    class ImageRetriever
    {
        #region enumeration
        public enum Source
        {
            earthPorn = 1,
            spacePorn = 2,
            wallpapers = 3
        }

        #endregion

        #region properties
        private int _selectedSource;
        public string _selectedSourceURL {
            get
            {
                switch (_selectedSource) {
                    case 1:
                        return "https://www.reddit.com/r/EarthPorn/";
                    case 2:
                        return "https://www.reddit.com/r/spaceporn/";
                    case 3:
                        return "https://www.reddit.com/r/wallpapers/";
                    default:
                        Random rnd = new Random();
                        _selectedSource = rnd.Next(1, 3);
                        return _selectedSourceURL;
                }
            }
        }
        #endregion

        #region constructor

        public ImageRetriever(Source selectedSource)
        {
            _selectedSource = (int) selectedSource;
        }

        #endregion

        #region public methods

        #endregion

        #region helper methods

        #endregion

    }
}
