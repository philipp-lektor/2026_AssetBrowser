using CommunityToolkit.Mvvm.ComponentModel;

namespace AssetBrowser.Models;

public partial class AssetItem : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string fileName = string.Empty;

    [ObservableProperty]
    private string assetType = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string createdBy = string.Empty;

    [ObservableProperty]
    private DateTime createdAt;

    [ObservableProperty]
    private bool isApproved;

    [ObservableProperty]
    private string thumbnailPath = string.Empty;
}
