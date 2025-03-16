
namespace YAssetsTrackerLibrary.DataAccess;

internal interface IAssetRequestData
{
   Task CreateAssetRequest(AssetRequestModel asset_request);
   Task<List<AssetRequestModel>> GetAllAssetRequests();
}