using System.Windows;
using AssetBrowser.Services;
using AssetBrowser.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AssetBrowser
{
    public partial class App : Application
    {
        private const string ThemeDictionaryPrefix = "Resources/Themes/";

        private ServiceProvider? serviceProvider;

        public void ApplyTheme(string themeName)
        {
            var appResources = Resources;
            var mergedDictionaries = appResources.MergedDictionaries;

            var currentThemeDictionary = mergedDictionaries
                .FirstOrDefault(dictionary => dictionary.Source is not null
                    && dictionary.Source.OriginalString.Contains(ThemeDictionaryPrefix, StringComparison.OrdinalIgnoreCase));

            if (currentThemeDictionary is not null)
            {
                mergedDictionaries.Remove(currentThemeDictionary);
            }

            mergedDictionaries.Insert(0, new ResourceDictionary
            {
                Source = new Uri($"Resources/Themes/{themeName}Theme.xaml", UriKind.Relative)
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            serviceProvider = ConfigureServices();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            serviceProvider?.Dispose();
            base.OnExit(e);
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAssetService, MockAssetService>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }
    }

}
