using AssetBrowser.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AssetBrowser.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAssetBrowserServices(this IServiceCollection services)
    {
        services.AddSingleton<IAssetService, MockAssetService>();
        services.AddSingleton<IThemeService, AppThemeService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        return services;
    }
}
