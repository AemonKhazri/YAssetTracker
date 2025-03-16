using Microsoft.Extensions.Caching.Memory;

namespace YAssetsTrackerLibrary.DataAccess
{
   class MongoAssetRequestData : IAssetRequestData
   {
      private readonly IMongoCollection<AssetRequestModel> _asset_requests;
      private readonly IMemoryCache _cache;
      private const string cacheName = "AssetRequestData";

      public MongoAssetRequestData(IDbConnection db, IMemoryCache cache)
      {
         _cache = cache;
         _asset_requests = db.AssetRequestCollection;
      }

      public async Task<List<AssetRequestModel>> GetAllAssetRequests()
      {

         var output = _cache.Get<List<AssetRequestModel>>(cacheName);


         if (output is null)
         {
            var results = await _asset_requests.FindAsync(_ => true);
            output = results.ToList();

            _cache.Set(cacheName, output, TimeSpan.FromDays(1));
         }
         return output;

      }


      public Task CreateAssetRequest(AssetRequestModel asset_request)
      {
         return _asset_requests.InsertOneAsync(asset_request);
      }

   }
}
