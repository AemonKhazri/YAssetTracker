using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAssetsTrackerLibrary.DataAccess
{
   public class MongoAssetData : IAssetData, IAssetData
   {
      private readonly IMongoCollection<AssetModel> _assets;
      private readonly IDbConnection _db;
      private readonly IUserData _userData;
      private readonly IMemoryCache _cache;
      private const string CacheName = "AssetData";

      public MongoAssetData(IDbConnection db, IUserData userData, IMemoryCache cache)
      {
         _assets = db.AssetCollection;
         _db = db;
         _userData = userData;
         _cache = cache;
      }
      public async Task<List<AssetModel>> GetAllAssets()
      {
         var output = _cache.Get<List<AssetModel>>(CacheName);


         if (output is null)
         {
            var results = await _assets.FindAsync(_ => true);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
         }
         return output;
      }
      public async Task CreateAsset(AssetModel asset)
      {
         var client = _db.Client;
         using var session = await client.StartSessionAsync();
         session.StartTransaction();
         try
         {
            var db = client.GetDatabase(_db.DbName);
            var assetsInTransactions = db.GetCollection<AssetModel>(_db.AssetCollectionName);
            await assetsInTransactions.InsertOneAsync(asset);

            var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(asset.CreatedBy.Id);
            user.AuthoredAssets.Add(new BasicAssetModel(asset));
            await userInTransaction.ReplaceOneAsync(u => u.Id == user.Id, user);

            await session.CommitTransactionAsync();
         }
         catch (Exception ex)
         {
            await session.AbortTransactionAsync();
            throw;

         }

      }

      public async Task<AssetModel> GetAsset(string id)
      {
         var results = await _assets.FindAsync(s => s.Id == id);
         return results.FirstOrDefault();
      }


      public async Task UpdateAsset(AssetModel asset)
      {
         await _assets.ReplaceOneAsync(s => s.Id == asset.Id, asset);
         _cache.Remove(CacheName);
      }




   }
}
