using System;
using System.Threading.Tasks;
using DBLauncher.Pages;
using DBLauncher.SaveWorks;
using DBLauncher.SaveWorks.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DBLauncher
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            Loaded += async (x, y) =>
            {
                GlobalContantsSaveHandler.InitGlobalConstants();
                
            };
        }

        private void NavigationViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(GamePage));
            NavigationViewControl.Header = nameof(GamePage);
        }

        private void NavigationViewControl_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                TryActivatePageByName(nameof(Pages) + "." + nameof(SettingsePage));
                SetNewHeader((Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem);
            }
            else
            {
                TryActivatePageByName(nameof(Pages) + "." + ((Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem).Tag.ToString());
                SetNewHeader((Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem);
            }
        }

        private void TryActivatePageByName(string tag)
        {
            try
            {
                ActivatePageByName(tag);
            }
            catch (Exception ex)
            {
                ExceptionDialog.ShowExceptionDialogPage(XamlRoot, ex.Message, ex.StackTrace);
            }
        }

        private void ActivatePageByName(string tag)
        {
            contentFrame.Navigate(Type.GetType($"{nameof(DBLauncher)}." + tag));
        }

        private void SetNewHeader(Microsoft.UI.Xaml.Controls.NavigationViewItem item)
        {
            NavigationViewControl.Header = item.Content.ToString();
        }
    }
}
