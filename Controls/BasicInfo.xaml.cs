using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace DBLauncher.Controls
{
    public sealed partial class BasicInfo : UserControl
    {
        private string key;
        private string value;
        private Color foregroundColor;
        
        public string Key { get => key; set => key = value; }
        public string Value { get => value; set => this.value = value; }
        public Color ForegroundColor { get => foregroundColor; set => foregroundColor = value; }

        public BasicInfo()
        {
            InitializeComponent();
        }
    }
}
