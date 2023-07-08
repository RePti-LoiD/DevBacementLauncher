using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gameloop.Vdf;
using Windows.UI.Xaml.Controls;

namespace DBLauncher.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                //Launcher.LaunchUriAsync(new Uri("steam://run/238320"));
            };
        }
    }
}