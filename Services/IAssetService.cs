using AssetBrowser.Models;

namespace AssetBrowser.Services;

public interface IAssetService
{
    IReadOnlyList<AssetItem> GetAssets();
}
