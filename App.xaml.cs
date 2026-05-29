using System.Windows;

namespace AssetBrowser
{
    public partial class App : Application
    {
        private const string ThemeDictionaryPrefix = "Resources/Themes/";

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
    }

}
