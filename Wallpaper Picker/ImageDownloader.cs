using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace Wallpaper_Picker
{
    class ImageDownloader
    {
        #region constructor
        public ImageDownloader(String downloadLocation)
        {
            _downloadLocation = downloadLocation;
        }
        #endregion

        #region properties
        private String _downloadLocation;
        public String _url { get; set; }
        public String _fileName { get; set; }
        #endregion

        #region public methods

        public void UpdateWallpaperFile(String url)
        {
            var urlParts = url.Split('/');
            this._url = url;
            this._fileName = String.Format("{0}\\{1}", _downloadLocation, urlParts[urlParts.Length - 1].Split('?')[0]);
            ClearFolder();
            Download();
        }

        #endregion

        #region helper methods
        private void Download()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(_url, _fileName);
            }
        }

        private void ClearFolder()
        {
            var directory = new DirectoryInfo(_downloadLocation);
            if (!directory.Exists)
            {
                directory.Create();

            }
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }
        #endregion
    }
}
