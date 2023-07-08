﻿using System.Collections.Generic;
using SteamApiTest.SteamApiData;

namespace SteamApiTest.Data
{
    public class SteamApiUserData
    {
        public string steamid
        {
            get; set;
        }
        public string personaname
        {
            get; set;
        }
        public string profileurl
        {
            get; set;
        }
        public string avatarfull
        {
            get; set;
        }

        public PersonaState personastate
        {
            get; set;
        }

        public CommunityVisibleState communityvisibilitystate
        {
            get; set;
        }
        public long lastlogoff
        {
            get; set;
        }
        public string realname
        {
            get; set;
        }
        public long timecreated
        {
            get; set;
        }
        public int gameid
        {
            get; set;
        }
        public string gameextrainfo
        {
            get; set;
        }
        public string loccountrycode
        {
            get; set;
        }
        public string locstatecode
        {
            get; set;
        }
        public int playerLevel
        {
            get; set;
        }
        public string profileBackground
        {
            get; set;
        }

        public SteamApiPlayerBans playerBans { get; set; } = new SteamApiPlayerBans();

        public CountryData СountryData = new CountryData();

        public List<int> lastPlayedGamesId = new List<int>();
        public List<SteamApiGameData> ownedGames = new List<SteamApiGameData>();
    }

    public class SteamApiPlayerBans
    {
        public bool CommunityBanned
        {
            get; set;
        }
        public bool VACBanned
        {
            get; set;
        }
        public int NumberOfVACBans
        {
            get; set;
        }
        public long DaysSinceLastBan
        {
            get; set;
        }
        public string EconomyBan
        {
            get; set;
        }
    }

    public enum PersonaState
    {
        Offline,
        Online,
        Busy,
        Away,
        Snooze,
        LookingForTrade,
        LookingToPlay
    }

    public enum CommunityVisibleState
    {
        PrivateOrFriendOnly = 1,
        Public = 3
    }
}