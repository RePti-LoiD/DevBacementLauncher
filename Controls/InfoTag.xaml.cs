using Windows.UI.Xaml.Controls;

namespace DBLauncher.Controls
{
    public sealed partial class InfoTag : UserControl
    {
        private string tagName;
        private string key;
        private bool isReadOnlyTag = true;

        public delegate void KeyChanged(InfoTag tag);
        public event KeyChanged onKeyChanged;

        public event KeyChanged OnKeyChanged
        {
            add => onKeyChanged += value;
            remove => onKeyChanged -= value;
        }

        public string TagName
        {
            get => tagName; set => tagName = value;
        }
        public string Key
        {
            get => key; set
            {
                EditBox.IsReadOnly = false;

                key = value;
                EditBox.Text = value;
                EditBox.IsReadOnly = isReadOnlyTag;
            }
        }
        public bool IsReadOnlyTag
        {
            get => isReadOnlyTag; set
            {
                EditBox.IsReadOnly = value; isReadOnlyTag = value;
            }
        }

        public InfoTag()
        {
            InitializeComponent();
        }

        private void EditBox_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!isReadOnlyTag) onKeyChanged?.Invoke(this);
        }
    }
}