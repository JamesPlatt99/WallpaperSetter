using System;

namespace Wallpaper_Picker
{
    class Program
    {
        const String _downloadLocation = @"c:\Wallpapers";

        static void Main(string[] args)
        {        
            var imageRetriever = new ImageRetriever(GetSelectedSource(args));
            var imageDownloader = new ImageDownloader(_downloadLocation);
            var wallpaperSetter = new WallpaperSetter(_downloadLocation);

            string url = imageRetriever.GetNewWallpaperURL();
            if(url != null)
            {
                imageDownloader.UpdateWallpaperFile(url);
                wallpaperSetter.UpdateWallpaper();
            }
        }

        static ImageRetriever.SubredditSource GetSelectedSource(string[] args){
            int output;
            if(args.Length > 0){
                int.TryParse(args[0], out output);
                return (ImageRetriever.SubredditSource)output;
            }
            return ImageRetriever.SubredditSource.random;
        }
    }
}
