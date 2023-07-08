using System.IO;
using Newtonsoft.Json;
using SteamAPI.Data;

namespace DBLauncher.SaveWorks.Data
{
    public class SteamApiDataSaveHandler
    {
        public static void SaveSteamDataHander()
        {
            File.WriteAllText(GlobalConstants.PathToSteamSaves, JsonConvert.SerializeObject(SteamApiDataHandler.UserData, Formatting.Indented));
        }
        
        /// <returns>true if file exists and data restored, false if file not found</returns>
        public static bool RestoreSteamDataHander()
        {
            if (File.Exists(GlobalConstants.PathToSteamSaves))
            {
                SteamApiDataHandler.UserData = (SteamApiUserData) JsonConvert.DeserializeObject(File.ReadAllText(GlobalConstants.PathToSteamSaves));
                return true;
            }
            else
            {
                File.CreateText(GlobalConstants.PathToSteamSaves);
                return false;
            }
        }
    }
}