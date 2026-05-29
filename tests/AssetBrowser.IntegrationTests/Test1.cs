using AssetBrowser.Services;
using AssetBrowser.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AssetBrowser.IntegrationTests;

public sealed class ServiceIntegrationTests
{
    [Fact]
    public void MockAssetService_ReturnsExpectedMockAssets()
    {
        var service = new MockAssetService();

        var assets = service.GetAssets();

        Assert.Equal(3, assets.Count);
        Assert.Equal("Mountain Sunrise", assets[0].Title);
        Assert.Equal("Product Launch Trailer", assets[1].Title);
        Assert.Equal("Brand Guidelines", assets[2].Title);
    }

    [Fact]
    public void AddAssetBrowserServices_RegistersCoreServices()
    {
        var services = new ServiceCollection();

        services.AddAssetBrowserServices();

        using var provider = services.BuildServiceProvider();

        var assetService = provider.GetService<IAssetService>();
        var themeService = provider.GetService<IThemeService>();
        var viewModel = provider.GetService<MainViewModel>();
        var mainWindowRegistration = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(MainWindow));

        Assert.NotNull(assetService);
        Assert.IsType<MockAssetService>(assetService!);
        Assert.NotNull(themeService);
        Assert.IsType<AppThemeService>(themeService!);
        Assert.NotNull(viewModel);
        Assert.Equal(3, viewModel!.Assets.Count);
        Assert.NotNull(mainWindowRegistration);
    }
}
