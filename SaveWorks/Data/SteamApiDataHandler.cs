using System;
using System.Collections.Generic;
using SteamAPI;
using SteamAPI.Data;

namespace DBLauncher.SaveWorks.Data
{
    internal static class SteamApiDataHandler
    {
        public static SteamApiUserData UserData = new SteamApiUserData();

        [NonSerialized]
        private static SteamApiDataParse steamApiDataParse = new SteamApiDataParse(GlobalConstants.SteamApiAccessKey, GlobalConstants.SteamUserID);

        public static SteamApiUserData GetUserData(bool parseOwnedGames)
        {
            SteamApiUserData userData = steamApiDataParse.GetUserInfo(parseOwnedGames);
            UserData = userData;

            SteamApiDataSaveHandler.SaveSteamDataHander(); 

            return userData;
        }

        public static List<SteamApiGameData> GetOwnedGames()
        {
            List<SteamApiGameData> steamApiGameDatas = steamApiDataParse.GetOwnedGamesData();
            UserData.ownedGames = steamApiGameDatas;

            SteamApiDataSaveHandler.SaveSteamDataHander();

            return steamApiGameDatas;
        }

        public static ExtendedGameData GetExtendedGameData(int appid)
        {
            ExtendedGameData extendedGameData = steamApiDataParse.GetExtendedGameData(appid);

            foreach (SteamApiGameData gameData in UserData.ownedGames)
                if (gameData.AppId == appid) gameData.ExtendedData = extendedGameData;
            
            SteamApiDataSaveHandler.SaveSteamDataHander();

            return extendedGameData;
        }

        public static List<SteamApiAchievement> GetAchievements(int appid)
        {
            List<SteamApiAchievement> extendedGameData = steamApiDataParse.GetAchievents(appid);

            foreach (SteamApiGameData gameData in UserData.ownedGames)
                if (gameData.AppId == appid) gameData.ExtendedData.SteamApiAchievements = extendedGameData;

            SteamApiDataSaveHandler.SaveSteamDataHander();

            return extendedGameData;
        }
    }
}