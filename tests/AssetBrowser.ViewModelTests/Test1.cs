using System.Threading;
using AssetBrowser.Models;
using AssetBrowser.Services;
using AssetBrowser.ViewModels;
using Xunit;

namespace AssetBrowser.ViewModelTests;

public sealed class MainViewModelTests
{
    [Fact]
    public void Constructor_LoadsAssets_AndSelectsFirstAsset()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();

            Assert.Equal(3, viewModel.Assets.Count);
            Assert.NotNull(viewModel.SelectedAsset);
            Assert.Equal("Mountain Sunrise", viewModel.SelectedAsset!.Title);
        });
    }

    [Fact]
    public void SearchText_FiltersAssetsByTitle()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();

            viewModel.SearchText = "mountain";

            var filteredAssets = viewModel.FilteredAssets.Cast<AssetItem>().ToList();

            Assert.Single(filteredAssets);
            Assert.Equal("Mountain Sunrise", filteredAssets[0].Title);
        });
    }

    [Fact]
    public void SearchText_FiltersAssetsByFileName()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();

            viewModel.SearchText = "brand-guidelines.pdf";

            var filteredAssets = viewModel.FilteredAssets.Cast<AssetItem>().ToList();

            Assert.Single(filteredAssets);
            Assert.Equal("Brand Guidelines", filteredAssets[0].Title);
        });
    }

    [Fact]
    public void Filters_ClearSelection_WhenSelectedAssetNoLongerMatches()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();
            viewModel.SelectedAsset = viewModel.Assets[0];

            viewModel.SelectedAssetTypeFilter = "Video";

            Assert.Null(viewModel.SelectedAsset);
            Assert.Single(viewModel.FilteredAssets.Cast<AssetItem>());
        });
    }

    [Fact]
    public void AddAssetCommand_AddsAndSelectsNewAsset()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();

            viewModel.AddAssetCommand.Execute(null);

            Assert.Equal(4, viewModel.Assets.Count);
            Assert.NotNull(viewModel.SelectedAsset);
            Assert.Equal("New Asset 4", viewModel.SelectedAsset!.Title);
        });
    }

    [Fact]
    public void DeleteSelectedAssetCommand_RemovesSelectedAsset()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();
            viewModel.SelectedAsset = viewModel.Assets[1];

            viewModel.DeleteSelectedAssetCommand.Execute(null);

            Assert.Equal(2, viewModel.Assets.Count);
            Assert.False(viewModel.Assets.Any(asset => asset.Title == "Product Launch Trailer"));
            Assert.NotNull(viewModel.SelectedAsset);
        });
    }

    [Fact]
    public void ClearSelectionCommand_ClearsSelectedAsset()
    {
        StaTestHelper.Run(() =>
        {
            var viewModel = CreateViewModel();

            viewModel.ClearSelectionCommand.Execute(null);

            Assert.Null(viewModel.SelectedAsset);
        });
    }

    private static MainViewModel CreateViewModel()
    {
        return new MainViewModel(new TestAssetService(CreateAssets()), new NoOpThemeService());
    }

    private static IReadOnlyList<AssetItem> CreateAssets()
    {
        return
        [
            new AssetItem
            {
                Title = "Mountain Sunrise",
                FileName = "mountain-sunrise.jpg",
                AssetType = "Image",
                Description = "A landscape photo used for a tourism campaign.",
                CreatedBy = "Emma Carter",
                CreatedAt = new DateTime(2025, 3, 12),
                IsApproved = true,
                ThumbnailPath = "https://picsum.photos/seed/mountain-sunrise/320/180"
            },
            new AssetItem
            {
                Title = "Product Launch Trailer",
                FileName = "launch-trailer.mp4",
                AssetType = "Video",
                Description = "Short teaser video for the new product release.",
                CreatedBy = "Liam Johnson",
                CreatedAt = new DateTime(2025, 4, 2),
                IsApproved = false,
                ThumbnailPath = "https://picsum.photos/seed/launch-trailer/320/180"
            },
            new AssetItem
            {
                Title = "Brand Guidelines",
                FileName = "brand-guidelines.pdf",
                AssetType = "Document",
                Description = "PDF document with logo, colors and typography rules.",
                CreatedBy = "Sophia Miller",
                CreatedAt = new DateTime(2025, 1, 18),
                IsApproved = true,
                ThumbnailPath = "https://picsum.photos/seed/brand-guidelines/320/180"
            }
        ];
    }

    private sealed class TestAssetService(IReadOnlyList<AssetItem> assets) : IAssetService
    {
        public IReadOnlyList<AssetItem> GetAssets()
        {
            return assets;
        }
    }

    private sealed class NoOpThemeService : IThemeService
    {
        public void ApplyTheme(string themeName)
        {
        }
    }

    private static class StaTestHelper
    {
        public static void Run(Action action)
        {
            Exception? capturedException = null;

            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    capturedException = ex;
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (capturedException is not null)
            {
                throw new Xunit.Sdk.XunitException($"STA test execution failed: {capturedException}");
            }
        }
    }
}
