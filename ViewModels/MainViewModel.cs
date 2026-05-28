using System.Collections.ObjectModel;
using AssetBrowser.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AssetBrowser.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteSelectedAssetCommand))]
    private AssetItem? selectedAsset;

    public ObservableCollection<AssetItem> Assets { get; } = new();

    public MainViewModel()
    {
        LoadMockData();
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
}
