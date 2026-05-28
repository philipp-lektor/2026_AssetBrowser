using System.Windows;
using AssetBrowser.ViewModels;

namespace AssetBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}