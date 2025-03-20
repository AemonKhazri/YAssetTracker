
namespace YAssetsTrackerLibrary.DataAccess;

public interface IAssetData
{
   Task CreateAsset(AssetModel asset);
   Task<List<AssetModel>> GetAllAssets();
}