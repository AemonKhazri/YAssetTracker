
namespace YAssetsTrackerLibrary.Models
{
   public  class BasicUserModel
    {

      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      public string UserName { get; set; }



      public BasicUserModel()
      {
         
      }
      public BasicUserModel(UserModel user)
      {
         Id = user.Id;
         UserName = user.UserName;
         
      }
   }
}
