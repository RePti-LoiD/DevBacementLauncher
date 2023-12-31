﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using SteamAPI.Data;
using DBLauncher.SaveWorks.Data;

namespace SteamAPI
{
    public class SteamApiDataParse
    {
        public static HttpClient httpClient = new HttpClient();

        public string steamApiKey = GlobalConstants.SteamApiAccessKey;
        public string userSteamID = GlobalConstants.SteamUserID;
        
        public static List<CountryData> countryData = new List<CountryData>();

        public SteamApiDataParse(string steamApiKey, string userSteamID)
        {
            this.steamApiKey = steamApiKey;
            this.userSteamID = userSteamID;
        }

        public SteamApiUserData GetUserInfo(bool parseOwnedGames = false)
        {
            string userData = httpClient.GetStringAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={steamApiKey}&steamids={userSteamID}").Result;
            string playerLevel = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetSteamLevel/v1/?key={steamApiKey}&steamid={userSteamID}").Result;
            string profileBackground = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetProfileBackground/v1/?key={steamApiKey}&steamid={userSteamID}").Result;
            string playerBans = httpClient.GetStringAsync($"https://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key={steamApiKey}&steamids={userSteamID}").Result;
            string lastPlayer = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v1/?key={steamApiKey}&steamid={userSteamID}&count=3").Result;
            Console.WriteLine("Catch request -> " + DateTime.Now.ToString("HH:mm:ss::fff"));

            JToken userDataObj = JObject.Parse(userData)["response"]["players"][0];
            SteamApiUserData steamApiUserData = userDataObj.ToObject<SteamApiUserData>();

            steamApiUserData.playerLevel = (int)JObject.Parse(playerLevel)["response"]["player_level"];

            steamApiUserData.profileBackground = GetIconLinkById((string)JObject.Parse(profileBackground)["response"]["profile_background"]["image_large"]);

            JToken playerBansObj = JObject.Parse(playerBans)["players"][0];


            JObject lastPlayedObj = JObject.Parse(lastPlayer);
            steamApiUserData.playerBans = new SteamApiPlayerBans()
            {
                CommunityBanned = (bool)playerBansObj["CommunityBanned"],
                VACBanned = (bool)playerBansObj["VACBanned"],
                NumberOfVACBans = (int)playerBansObj["DaysSinceLastBan"],
                DaysSinceLastBan = (int)playerBansObj["CommunityBanned"],
                EconomyBan = (string)playerBansObj["EconomyBan"],
            };

            if (lastPlayedObj["response"]["games"] != null)
                for (int i = 0; i < (int)lastPlayedObj["response"]["total_count"]; i++)
                    steamApiUserData.lastPlayedGamesId.Add((int)lastPlayedObj["response"]["games"][i]["appid"]);

            foreach (CountryData item in countryData)
                if (item.Alpha2 == steamApiUserData.loccountrycode)
                {
                    steamApiUserData.СountryData = item;
                    break;
                }

            if (parseOwnedGames)
                steamApiUserData.ownedGames = GetOwnedGamesData();

            return steamApiUserData;
        }

        public List<SteamApiGameData> GetOwnedGamesData()
        {
            string gameData = httpClient.GetStringAsync($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={steamApiKey}&steamid={userSteamID}&format=json&include_appinfo=true&skip_unvetted_apps=true").Result;
            JToken gameDataObj = JObject.Parse(gameData)["response"]["games"];

            if (gameDataObj == null) return null;

            List<SteamApiGameData> gamesData = new List<SteamApiGameData>();
            for (int i = 0; i < gameDataObj.ToList().Count; i++)
            {
                SteamApiGameData steamApiGameData = new SteamApiGameData()
                {
                    AppId = (int)gameDataObj[i]["appid"],
                    Name = (string)gameDataObj[i]["name"],
                    PlaytimeForever = (int)gameDataObj[i]["playtime_forever"],
                    ImgIconUrl = (string)gameDataObj[i]["img_icon_url"],
                    RTimeLastPlayed = (int)gameDataObj[i]["rtime_last_played"],
                };

                gamesData.Add(steamApiGameData);
            }

            foreach (var item in gamesData)
                if (GlobalConstants.SteamPathsData.Contains($"{'"'}{item.AppId}{'"'}"))
                    item.IsDownloaded = true;

            return gamesData;
        }

        public ExtendedGameData GetExtendedGameData(int appid)
        {
            string appdetails = httpClient.GetStringAsync($"https://store.steampowered.com/api/appdetails?appids={appid}").Result;

            JToken extendedDataObj = JObject.Parse(appdetails)[appid.ToString()];

            if (!(bool)extendedDataObj["success"]) return null;
            JToken extendedDataObjData = extendedDataObj["data"];


            ExtendedGameData extendedData = new ExtendedGameData()
            {
                required_age = (int)extendedDataObjData["required_age"],
                controller_support = (string)extendedDataObjData["controller_support"],
                detailed_description = (string)extendedDataObjData["detailed_description"],
                short_description = (string)extendedDataObjData["short_description"],
                supported_languages = (string)extendedDataObjData["supported_languages"],
                header_image = (string)extendedDataObjData["header_image"],
                release_date = (string)extendedDataObjData["release_date"]["date"],
                pc_requirements = new PcRequirements()
                {
                    minimum = (string)extendedDataObjData["pc_requirements"]["minimum"],
                    recommended = (string)extendedDataObjData["pc_requirements"]["recommended"] == null ? "Not set" : (string)extendedDataObj["data"]["pc_requirements"]["recommended"]
                }
            };
            extendedData.developers = extendedDataObjData["developers"].ToObject<string[]>();
            extendedData.publishers = extendedDataObjData["publishers"].ToObject<string[]>();


            string playerCount = httpClient.GetStringAsync($"https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v0001/?key={steamApiKey}&steamid={userSteamID}&appid={appid}&format=json").Result;
            extendedData.player_count = (int)JObject.Parse(playerCount)["response"]["player_count"];

            try
            {
                for (int i = 0; i < extendedDataObjData["categories"].ToList().Count; i++)
                    extendedData.categories.Add((string)extendedDataObjData["categories"][i]["description"]);
            }
            catch { }
            try
            {
                for (int i = 0; i < extendedDataObjData["genres"].ToList().Count; i++)
                    extendedData.genres.Add((string)extendedDataObjData["genres"][i]["description"]);
            }
            catch { }

            try
            {
                for (int i = 0; i < extendedDataObjData["screenshots"].ToList().Count; i++)
                    extendedData.screenshotsLinks.Add((string)extendedDataObjData["screenshots"][i]["path_full"]);
            }
            catch { }
            try
            {
                extendedData.SteamApiAchievements = GetAchievents(appid);
            }
            catch { }

            return extendedData;
        }

        public List<SteamApiAchievement> GetAchievents(int appid)
        {
            string schemaForGame = httpClient.GetStringAsync($"https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?key={steamApiKey}&appid={appid}").Result;
            JToken schemaForGameRoot = JObject.Parse(schemaForGame)["game"]["availableGameStats"];

            if (schemaForGameRoot == null) return null;

            JToken schemaForGameObj = schemaForGameRoot["achievements"];

            string globalPercentage = httpClient.GetStringAsync($"https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v2/?key={steamApiKey}&gameid={appid}").Result;
            JToken globalPercentageObj = JObject.Parse(globalPercentage)["achievementpercentages"]["achievements"];

            string playerStats = httpClient.GetStringAsync($"https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v1/?key={steamApiKey}&steamid={userSteamID}&appid={appid}").Result;
            JToken playerStatsObj = JObject.Parse(playerStats)["playerstats"]["achievements"];

            List<SteamApiAchievement> achievements = new List<SteamApiAchievement>();
            for (int i = 0; i < schemaForGameObj.ToObject<List<JToken>>().Count; i++)
            {
                achievements.Add(new SteamApiAchievement()
                {
                    ApiName = (string)playerStatsObj[i]["apiname"],
                    DisplayName = (string)schemaForGameObj[i]["displayName"],
                    Description = (string)schemaForGameObj[i]["description"],
                    Icon = (string)schemaForGameObj[i]["icon"],
                    IconGray = (string)schemaForGameObj[i]["icongray"],

                    Achieved = (bool)playerStatsObj[i]["achieved"],
                    UnlockTime = (long)playerStatsObj[i]["unlocktime"]
                });
            }

            int percentAchievementCount = globalPercentageObj.ToList().Count;
            foreach (SteamApiAchievement achievement in achievements)
            {
                for (int i = 0; i < percentAchievementCount; i++)
                {
                    if (achievement.ApiName == (string)globalPercentageObj[i]["name"])
                    {
                        achievement.AchievementPercent = (float)globalPercentageObj[i]["percent"];
                        break;
                    }
                }
            }

            return achievements;
        }

        public static string GetIconLinkById(int appid, string hash) => $"http://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{hash}.jpg";
        public static string GetIconLinkById(string hash) => $"http://media.steampowered.com/steamcommunity/public/images/{hash}";
        public static DateTime GetTimeByStamp(long stamp) => DateTimeOffset.FromUnixTimeSeconds(stamp).Date;
    }
}