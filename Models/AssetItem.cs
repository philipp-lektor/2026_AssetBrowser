namespace AssetBrowser.Models;

public class AssetItem
{
    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string AssetType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public bool IsApproved { get; set; }

    public string ThumbnailPath { get; set; } = string.Empty;
}
