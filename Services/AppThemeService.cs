using System.Windows;

namespace AssetBrowser.Services;

public class AppThemeService : IThemeService
{
    public void ApplyTheme(string themeName)
    {
        if (Application.Current is App app)
        {
            app.ApplyTheme(themeName);
        }
    }
}
