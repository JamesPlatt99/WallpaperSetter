using System;

namespace Wallpaper_Picker
{
    class Program
    {
        static void Main(string[] args)
        {
            var imageRetriever = new ImageRetriever(SelectRandomSource());
        }

        static ImageRetriever.Source SelectRandomSource()
        {
            var random = new Random();
            int selectedSource = random.Next(1, Enum.GetNames(typeof(ImageRetriever.Source)).Length);
            return (ImageRetriever.Source)selectedSource;
        }
    }
}
