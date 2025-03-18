using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAssetsTrackerLibrary.Models
{
    public class BasicAssetModel
    {
      
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      public string Title { get; set; }

      public BasicAssetModel()
      {
         
      }

      public BasicAssetModel(AssetModel asset)
      {
         Id = asset.Id;
         Title = asset.Title;
         
      }



   }
   
}
