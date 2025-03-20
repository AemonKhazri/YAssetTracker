using Microsoft.Extensions.Caching.Memory;

namespace YAssetsTrackerLibrary.DataAccess
{
   public class MongoAssetData : IAssetData
   {
      private readonly IMongoCollection<AssetModel> _assets;
      private readonly IMemoryCache _cache;
      private const string CacheName = "AssetData";
      public MongoAssetData(IDbConnection db, IMemoryCache cache)
      {
         _cache = cache;
         _assets = db.AssetCollection;

      }
      public async Task<List<AssetModel>> GetAllAssets()
      {
         var output = _cache.Get<List<AssetModel>>(CacheName);
         if (output is null)
         {
            var results = await _assets.FindAsync(_ => true);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromDays(1));
         }
         return output;


      }
      public Task CreateAsset(AssetModel asset)
      {
         return _assets.InsertOneAsync(asset);
      }

   }
}
