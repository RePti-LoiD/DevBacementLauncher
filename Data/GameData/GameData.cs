using System;
using System.Collections.Generic;

namespace DBLauncher.Data.GameData
{
    public class GameData
    {
        private string gameName, gameStatus;
        private DateTime lastLaunch;
        private TimeSpan gameHours;
        private (int, int) achievements;

        private List<string> imagePaths;
        private List<InfoHourData> infoHoursData;
    }
}