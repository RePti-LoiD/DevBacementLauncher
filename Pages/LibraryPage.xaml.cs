using Windows.UI.Xaml.Controls;

namespace DBLauncher.Pages
{
    public sealed partial class LibraryPage : Page
    {
        public LibraryPage()
        {
            InitializeComponent();
        }

        private void MainView_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;

            if (scrollViewer.VerticalOffset >= 500 && MainInfoGridParent.Children.Contains(MainInfoGrid))
            {
                MainInfoGridParent.Children.Remove(MainInfoGrid);
                RootGrid.Children.Add(MainInfoGrid);
            }
            else if (scrollViewer.VerticalOffset < 500 && RootGrid.Children.Contains(MainInfoGrid))
            {
                RootGrid.Children.Remove(MainInfoGrid);
                MainInfoGridParent.Children.Add(MainInfoGrid);
            }
        }
    }
}
