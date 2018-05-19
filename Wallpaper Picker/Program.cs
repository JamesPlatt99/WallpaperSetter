using System;

namespace Wallpaper_Picker
{
    class Program
    {
        const String _downloadLocation = @"c:\Wallpapers";

        static void Main(string[] args)
        {
            var imageRetriever = new ImageRetriever();
            var imageDownloader = new ImageDownloader(_downloadLocation);
            var wallpaperSetter = new WallpaperSetter(_downloadLocation);

            string url = imageRetriever.GetNewWallpaperURL();
            if(url != null)
            {
                imageDownloader.UpdateWallpaperFile(url);
                wallpaperSetter.UpdateWallpaper();
            }
        }
    }
}
