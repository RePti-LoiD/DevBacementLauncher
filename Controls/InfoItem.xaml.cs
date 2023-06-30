using Windows.UI.Xaml.Controls;

namespace DBLauncher.Controls
{
    public sealed partial class InfoItem : UserControl
    {
        private string header, info, infoIcon;

        public string Header
        {
            get => header;
            set => header = value;
        }
        public string Info
        {
            get => info;
            set => info = value;
        }
        public string InfoIcon
        {
            get => infoIcon;
            set => infoIcon = value;
        }

        public InfoItem()
        {
            InitializeComponent();
        }
    }
}