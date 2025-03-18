

namespace YAssetsTrackerLibrary.Models
{
    public class AssetModel
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
       public string Id { get; set; }
      public string Title { get; set; }
      public BasicUserModel CreatedBy { get; set; }
      public AssetRequestModel Request { get; set; }
      public string Description { get; set; }
      public string AssignedTo { get; set; }
      public string Department { get; set; }
      public DateTime AssignmentDate { get; set; }
      public DateTime ReplacementDate { get; set; }
   }
}
