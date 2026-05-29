using System.Windows;
using AssetBrowser.ViewModels;

namespace AssetBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}