using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using DBLauncher.SaveWorks.Data;
using SteamAPI.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using DBLauncher.SaveWorks;

namespace DBLauncher.Pages
{
    public sealed partial class AccountPage : Page
    {
        private SteamApiUserData userData = SteamApiDataHandler.UserData;

        public AccountPage()
        {
            InitializeComponent();

            Loaded += async (x, y) =>
            {
                GlobalContantsSaveHandler.InitGlobalConstants();

                await Task.Run(() => SteamApiDataHandler.GetUserData(false));
                userData = SteamApiDataHandler.UserData;

                AvatarImage.Source = new BitmapImage(new Uri(userData.avatarfull));
                BackgroundImage.Source = new BitmapImage(new Uri(userData.profileBackground));

                Status.Foreground = GetColorByState();
                if (userData.gameextrainfo != null) InGame.Visibility = Visibility.Visible;
                if (userData.personastate == PersonaState.Online) LastLogOff.Visibility = Visibility.Collapsed;

                if (userData.playerBans.VACBanned == true) IsVacBanned.Foreground = Application.Current.Resources["SystemFillColorCriticalBrush"] as Brush;
                else IsVacBanned.Foreground = Application.Current.Resources["SystemFillColorSuccessBrush"] as Brush;


                Bindings.Update();
                SteamApiDataSaveHandler.SaveSteamDataHander();
            };
        }

        private Brush GetColorByState()
        {
            Brush brush = null;

            switch (userData.personastate)
            {
                case PersonaState.Offline:
                    brush = Application.Current.Resources["TextFillColorDisabledBrush"] as Brush;
                    break;
                case PersonaState.Online:
                    brush = Application.Current.Resources["SystemFillColorSuccessBrush"] as Brush;
                    break;
                case PersonaState.Busy:
                    brush = Application.Current.Resources["AccentTextFillColorTertiaryBrush"] as Brush;
                    break;
                case PersonaState.Away:
                    brush = Application.Current.Resources["AccentTextFillColorPrimaryBrush"] as Brush;
                    break;
                case PersonaState.Snooze:
                    brush = Application.Current.Resources["TextFillColorDisabledBrush"] as Brush;
                    break;
                case PersonaState.LookingForTrade:
                    brush = Application.Current.Resources["SystemFillColorSuccessBrush"] as Brush;
                    break;
                case PersonaState.LookingToPlay:
                    brush = Application.Current.Resources["SystemFillColorSuccessBrush"] as Brush;
                    break;
                default:
                    brush = Application.Current.Resources["TextFillColorDisabledBrush"] as Brush;
                    break;
            }

            return brush;
        }
    }
}