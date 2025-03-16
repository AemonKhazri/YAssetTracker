using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAssetsTrackerLibrary.DataAccess
{
   public class MongoAssetData : IAssetData
   {
      private readonly IMongoCollection<AssetModel> _assets;

      public MongoAssetData(IDbConnection db)
      {
         _assets = db.AssetCollection;

      }
      public async Task<List<AssetModel>> GetAllAssets()
      {
         var results = await _assets.FindAsync(_ => true);
         return results.ToList();
      }
      public Task CreateAsset(AssetModel asset)
      {
         return _assets.InsertOneAsync(asset);
      }

   }
}
