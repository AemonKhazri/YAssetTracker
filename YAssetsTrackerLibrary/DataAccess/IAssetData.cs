
namespace YAssetsTrackerLibrary.DataAccess;

public interface IAssetData
{
   Task CreateAsset(AssetModel asset);
   Task<List<AssetModel>> GetAllAssets();
   Task<AssetModel> GetAsset(string id);
   Task UpdateAsset(AssetModel asset);
}