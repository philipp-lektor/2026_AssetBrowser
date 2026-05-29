using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using AssetBrowser.Models;
using AssetBrowser.Services;
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

    [ObservableProperty]
    private string selectedTheme = "Light";

    public ObservableCollection<AssetItem> Assets { get; } = new();

    // ICollectionView is a simple WPF way to add search and filter behavior
    // without replacing the original collection.
    public ICollectionView FilteredAssets { get; }

    public IReadOnlyList<string> AssetTypes { get; } = ["Image", "Video", "Document", "Graphic"];

    public IReadOnlyList<string> AssetTypeFilters { get; } = ["All", "Image", "Video", "Document", "Graphic"];

    public IReadOnlyList<string> ApprovalFilters { get; } = ["All", "Approved", "Not Approved"];

    public IReadOnlyList<string> Themes { get; } = ["Light", "Dark"];

    private readonly IAssetService assetService;
    private readonly IThemeService themeService;

    public MainViewModel(IAssetService assetService, IThemeService themeService)
    {
        this.assetService = assetService;
        this.themeService = themeService;

        LoadAssets();

        FilteredAssets = CollectionViewSource.GetDefaultView(Assets);
        FilteredAssets.Filter = FilterAsset;
    }

    private void LoadAssets()
    {
        foreach (var asset in assetService.GetAssets())
        {
            RegisterAsset(asset);
        }

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
            ThumbnailPath = $"https://picsum.photos/seed/asset-{Assets.Count + 1}/320/180"
        };

        RegisterAsset(newAsset);
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

        UnregisterAsset(assetToRemove);
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

    partial void OnSelectedThemeChanged(string value)
    {
        themeService.ApplyTheme(value);
    }

    private void RefreshFilters()
    {
        FilteredAssets.Refresh();

        if (SelectedAsset is not null && !MatchesFilter(SelectedAsset))
        {
            SelectedAsset = null;
        }
    }

    private void RegisterAsset(AssetItem asset)
    {
        asset.PropertyChanged += Asset_PropertyChanged;
        Assets.Add(asset);
    }

    private void UnregisterAsset(AssetItem asset)
    {
        asset.PropertyChanged -= Asset_PropertyChanged;
    }

    private void Asset_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        RefreshFilters();
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
