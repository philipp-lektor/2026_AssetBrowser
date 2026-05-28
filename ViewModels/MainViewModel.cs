using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AssetBrowser.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AssetBrowser.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteSelectedAssetCommand))]
    private AssetItem? selectedAsset;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private string selectedAssetTypeFilter = "All";

    [ObservableProperty]
    private string selectedApprovalFilter = "All";

    public ObservableCollection<AssetItem> Assets { get; } = new();

    // ICollectionView is a simple WPF way to add search and filter behavior
    // without replacing the original collection.
    public ICollectionView FilteredAssets { get; }

    public IReadOnlyList<string> AssetTypes { get; } = ["Image", "Video", "Document", "Graphic"];

    public IReadOnlyList<string> AssetTypeFilters { get; } = ["All", "Image", "Video", "Document", "Graphic"];

    public IReadOnlyList<string> ApprovalFilters { get; } = ["All", "Approved", "Not Approved"];

    public MainViewModel()
    {
        LoadMockData();

        FilteredAssets = CollectionViewSource.GetDefaultView(Assets);
        FilteredAssets.Filter = FilterAsset;
    }

    private void LoadMockData()
    {
        Assets.Add(new AssetItem
        {
            Title = "Mountain Sunrise",
            FileName = "mountain-sunrise.jpg",
            AssetType = "Image",
            Description = "A landscape photo used for a tourism campaign.",
            CreatedBy = "Emma Carter",
            CreatedAt = new DateTime(2025, 3, 12),
            IsApproved = true,
            ThumbnailPath = "Assets/Images/mountain-sunrise.jpg"
        });

        Assets.Add(new AssetItem
        {
            Title = "Product Launch Trailer",
            FileName = "launch-trailer.mp4",
            AssetType = "Video",
            Description = "Short teaser video for the new product release.",
            CreatedBy = "Liam Johnson",
            CreatedAt = new DateTime(2025, 4, 2),
            IsApproved = false,
            ThumbnailPath = "Assets/Videos/launch-trailer.png"
        });

        Assets.Add(new AssetItem
        {
            Title = "Brand Guidelines",
            FileName = "brand-guidelines.pdf",
            AssetType = "Document",
            Description = "PDF document with logo, colors and typography rules.",
            CreatedBy = "Sophia Miller",
            CreatedAt = new DateTime(2025, 1, 18),
            IsApproved = true,
            ThumbnailPath = "Assets/Documents/brand-guidelines.png"
        });

        SelectedAsset = Assets.FirstOrDefault();
    }

    [RelayCommand]
    private void AddAsset()
    {
        var newAsset = new AssetItem
        {
            Title = $"New Asset {Assets.Count + 1}",
            FileName = "new-file.ext",
            AssetType = "Image",
            Description = "Add a short description here.",
            CreatedBy = "Student",
            CreatedAt = DateTime.Today,
            IsApproved = false,
            ThumbnailPath = string.Empty
        };

        Assets.Add(newAsset);
        RefreshFilters();
        SelectedAsset = newAsset;
    }

    [RelayCommand(CanExecute = nameof(CanDeleteSelectedAsset))]
    private void DeleteSelectedAsset()
    {
        if (SelectedAsset is null)
        {
            return;
        }

        var assetToRemove = SelectedAsset;
        var assetIndex = Assets.IndexOf(assetToRemove);

        Assets.Remove(assetToRemove);
        RefreshFilters();

        if (Assets.Count == 0)
        {
            SelectedAsset = null;
            return;
        }

        var nextIndex = Math.Min(assetIndex, Assets.Count - 1);
        SelectedAsset = Assets[nextIndex];
    }

    private bool CanDeleteSelectedAsset()
    {
        return SelectedAsset is not null;
    }

    [RelayCommand]
    private void ClearSelection()
    {
        SelectedAsset = null;
    }

    partial void OnSearchTextChanged(string value)
    {
        RefreshFilters();
    }

    partial void OnSelectedAssetTypeFilterChanged(string value)
    {
        RefreshFilters();
    }

    partial void OnSelectedApprovalFilterChanged(string value)
    {
        RefreshFilters();
    }

    private void RefreshFilters()
    {
        FilteredAssets.Refresh();

        if (SelectedAsset is not null && !MatchesFilter(SelectedAsset))
        {
            SelectedAsset = null;
        }
    }

    private bool FilterAsset(object item)
    {
        return item is AssetItem asset && MatchesFilter(asset);
    }

    private bool MatchesFilter(AssetItem asset)
    {
        var matchesSearch = string.IsNullOrWhiteSpace(SearchText)
            || asset.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            || asset.FileName.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

        var matchesType = SelectedAssetTypeFilter == "All"
            || asset.AssetType.Equals(SelectedAssetTypeFilter, StringComparison.OrdinalIgnoreCase);

        var matchesApproval = SelectedApprovalFilter switch
        {
            "Approved" => asset.IsApproved,
            "Not Approved" => !asset.IsApproved,
            _ => true
        };

        return matchesSearch && matchesType && matchesApproval;
    }
}
