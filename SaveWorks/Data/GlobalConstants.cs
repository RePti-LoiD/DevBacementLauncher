using System.IO;
using Windows.Storage;

namespace DBLauncher.SaveWorks.Data
{
    public static class GlobalConstants
    {
        private static string steamApiAccessKey = "98E74DA08813E17F9F1D89EB9538B7E2";
        private static string steamUserID;
        private static string steamPathsData = Path.Combine("C:", "Program Files (x86)", "Steam", "steamapps", "libraryfolders.vdf");

        public static string SteamApiAccessKey => steamApiAccessKey;
        public static string SteamUserID => steamUserID;
        public static string SteamPathsData => steamPathsData;

        public static readonly string PathToSteamSaves = Path.Combine(ApplicationData.Current.LocalFolder.Path, "SteamSaves.json");
        public static readonly string PathToInnerConstants = Path.Combine(ApplicationData.Current.LocalFolder.Path, "SteamUserID.dbid");

        public static void SetSteamUserID(string steamUserID)
        {
            GlobalConstants.steamUserID = steamUserID;
            GlobalContantsSaveHandler.SaveData();
        }

        public static void SetSteamDataPath(string steamDataPath)
        {
            steamPathsData = steamDataPath;
            GlobalContantsSaveHandler.SaveData();
        }
    }
}