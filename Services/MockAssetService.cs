using AssetBrowser.Models;

namespace AssetBrowser.Services;

public class MockAssetService : IAssetService
{
    public IReadOnlyList<AssetItem> GetAssets()
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
}
