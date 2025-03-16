
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace YAssetsTrackerLibrary.DataAccess
{
    public class DbConnection
    {
      private readonly IConfiguration _config;
      private readonly IMongoDatabase _db;
      private string _connectionId = "MongoDB";
      public string DbName { get; private set; }

      public string UserCollectionName { get; private set; } = "users";
      public string AssetCollectionName { get; private set; } = "assets";
      public string AssetRequestName { get; private set; } = "asset_requests";

      public MongoClient Client { get; private set; }

      public IMongoCollection<UserModel> UserCollection { get; private set; }
      public IMongoCollection<AssetModel> AssetCollection { get; private set; }
      public IMongoCollection<AssetRequestModel> AssetRequestCollection { get; private set; }
      public DbConnection(IConfiguration config)
      {
         _config = config;
         Client = new MongoClient(_config.GetConnectionString(_connectionId));
         DbName = _config["DatabaseName"];
         _db = Client.GetDatabase(DbName);

         UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
         AssetCollection = _db.GetCollection<AssetModel>(AssetCollectionName);
         AssetRequestCollection = _db.GetCollection<AssetRequestModel>(AssetRequestName);

      }
   }
}
