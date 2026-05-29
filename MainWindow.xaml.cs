using System.Windows;
using AssetBrowser.Services;
using AssetBrowser.ViewModels;

namespace AssetBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow(IAssetService assetService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(assetService);
        }
    }
}