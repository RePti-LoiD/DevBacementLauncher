using Windows.UI.Xaml.Controls;

namespace DBLauncher.Controls
{
    public sealed partial class InfoHour : UserControl
    {
        private int hours;
        private string category;

        public int Hours
        {
            get => hours;
            set => hours = value;
        }
        public string Category
        {
            get => category;
            set => category = value;
        }

        public InfoHour()
        {
            InitializeComponent();
        }
    }
}