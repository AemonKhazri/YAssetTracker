using MongoDB.Driver;

namespace YAssetsTrackerLibrary.DataAccess;
public interface IDbConnection
{
   IMongoCollection<AssetModel> AssetCollection { get; }
   string AssetCollectionName { get; }
   IMongoCollection<AssetRequestModel> AssetRequestCollection { get; }
   string AssetRequestName { get; }
   MongoClient Client { get; }
   string DbName { get; }
   IMongoCollection<UserModel> UserCollection { get; }
   string UserCollectionName { get; }
}