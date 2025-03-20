using Microsoft.Extensions.Caching.Memory;

namespace YAssetsTrackerLibrary.DataAccess
{
   public class MongoAssetData : IAssetData
   {
      private readonly IMongoCollection<AssetModel> _assets;
      private readonly IMemoryCache _cache;
      private const string CacheName = "AssetData";
      private readonly IDbConnection _db;
      private readonly IUserData _userData;
      public MongoAssetData(IDbConnection db,IUserData userData, IMemoryCache cache)
      {
         _cache = cache;
         _assets = db.AssetCollection;
         _db = db;
         _userData = userData;

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
      public async  Task CreateAsset(AssetModel asset)
      {
         var client = _db.Client;
         using var session = await client.StartSessionAsync();
         session.StartTransaction();
         try
         {
            var db = client.GetDatabase(_db.DbName);
            var assetsInTransaction = db.GetCollection<AssetModel>(_db.AssetCollectionName);
            await assetsInTransaction.InsertOneAsync(asset);


            var userInTransaction = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(asset.CreatedBy.Id);
            user.AuthoredAssets.Add(new BasicAssetModel(asset));
            await userInTransaction.ReplaceOneAsync(u => u.Id == user.Id, user);

            await session.CommitTransactionAsync();

         }
         catch(Exception ex)
         {
            await session.AbortTransactionAsync();
            throw;
         }
         //return _assets.InsertOneAsync(asset);
      }

   }
}
