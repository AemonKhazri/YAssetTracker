
namespace YAssetsTrackerLibrary.DataAccess;

public interface IAssetRequestData
{
   Task CreateAssetRequest(AssetRequestModel asset_request);
   Task<List<AssetRequestModel>> GetAllAssetRequests();
}