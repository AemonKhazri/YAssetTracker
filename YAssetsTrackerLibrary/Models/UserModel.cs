

namespace YAssetsTrackerLibrary.Models
{
   public  class UserModel
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      public string UserName { get; set; }
      //the id that comes from Azure
      public string ObjectIdentifier { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
    
      public string EmailAddress { get; set; }

      public List<BasicAssetModel> AuthoredAssets { get; set; } = new();
   }
}
