

namespace YAssetsTrackerLibrary.Models
{
    public class AssetRequestModel
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      public string Ritm { get; set; }
      public string CoupaNumber { get; set; }
      public double Value { get; set; } 
      public DateTime RequestDate { get; set; } = DateTime.UtcNow;
      //this is this author
      public BasicUserModel CreatedBy { get; set; } 


   }
}
