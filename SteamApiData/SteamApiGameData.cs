using System.Collections.Generic;

namespace SteamApiTest.Data
{

    public class SteamApiGameData
    {
        public int AppId
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public int PlaytimeForever
        {
            get; set;
        }
        public string ImgIconUrl
        {
            get; set;
        }
        public long RTimeLastPlayed
        {
            get; set;
        }
        public bool IsDownloaded
        {
            get; set;
        }
        public int AchievementsCount
        {
            get; set;
        }
        public int AchievementsReached
        {
            get; set;
        }
        public ExtendedData ExtendedData { get; set; } = new ExtendedData();
    }

    public class ExtendedData
    {
        public int required_age;
        public string controller_support;
        public string detailed_description;
        public string short_description;
        public string supported_languages;
        public string header_image;

        public int player_count
        {
            get; set;
        }

        public PcRequirements pc_requirements;
        public string release_date;

        public string[] developers;
        public string[] publishers;
        public List<string> categories = new List<string>();
        public List<string> genres = new List<string>();

        public List<string> screenshotsLinks = new List<string>();
        public List<SteamApiAchievement> SteamApiAchievements { get; set; } = new List<SteamApiAchievement>();
    }

    public class SteamApiAchievement
    {
        public string ApiName
        {
            get; set;
        }
        public string DisplayName
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public string Icon
        {
            get; set;
        }
        public string IconGray
        {
            get; set;
        }
        public bool Achieved
        {
            get; set;
        }
        public long UnlockTime
        {
            get; set;
        }
        public float AchievementPercent
        {
            get; set;
        }
    }

    public class PcRequirements
    {
        public string minimum;
        public string recommended;
    }

    public class Metacritic
    {
        public int Score;
        public string Link;
    }
}