using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DBLauncher
{
    public static class ExceptionDialog
    {
        public static void ShowExceptionDialogPage(XamlRoot xamlRoot)
        {
            ShowExceptionDialogPage(xamlRoot, "UnknownException");
        }

        public static void ShowExceptionDialogPage(XamlRoot xamlRoot, string label)
        {
            ShowExceptionDialogPage(xamlRoot, label, "No StackTrace");
        }

        public static async void ShowExceptionDialogPage(XamlRoot xamlRoot, string label, string stackTrace)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = xamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = label;
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = stackTrace;

            await dialog.ShowAsync(); 
        }
    }
}