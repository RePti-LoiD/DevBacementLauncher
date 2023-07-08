using System.IO;
using DBLauncher.SaveWorks.Data;

namespace DBLauncher.SaveWorks
{
    public static class GlobalContantsSaveHandler
    {
        public static void SaveData() => File.WriteAllText(GlobalConstants.PathToInnerConstants, $"{GlobalConstants.SteamUserID},{GlobalConstants.SteamPathsData}");
        
        /// <returns>true if file exists and data restored, false if file not found</returns>
        public static bool InitGlobalConstants()
        {
            if (File.Exists(GlobalConstants.PathToInnerConstants))
            {
                string[] data = File.ReadAllText(GlobalConstants.PathToInnerConstants).Split(',');
                GlobalConstants.SetSteamUserID(data[0]);
                GlobalConstants.SetSteamDataPath(data[1]);

                return true;
            }
            else
            {
                SaveData();

                return false;
            }
        }
    }
}