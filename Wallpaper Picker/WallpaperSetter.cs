using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.ComponentModel;

namespace Wallpaper_Picker
{
    class WallpaperSetter
    {
        #region constructor
        public WallpaperSetter(String directory)
        {
            this._directory = directory;
        }
        #endregion

        #region properties
        private String _directory;
        const int SetDeskWallpaper = 20;
        const int UpdateIniFile = 0x01;
        const int sendWinIniChange = 0x02;
        #endregion

        #region public methods
        public void UpdateWallpaper()
        {
            SystemParametersInfo(SetDeskWallpaper, 0, GetFile().FullName, UpdateIniFile | sendWinIniChange);
        }
        #endregion

        #region helper methods
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uaction, int uParam, string lpvParam, int fuWinIni);

        private FileInfo GetFile()
        {
            var dir = new DirectoryInfo(_directory);
            return dir.GetFiles()[0];
        }
        #endregion

    }
}
