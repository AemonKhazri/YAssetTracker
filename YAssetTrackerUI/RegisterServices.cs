namespace YAssetTrackerUI;

public static class RegisterServices
{
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {

      builder.Services.AddRazorComponents()
          .AddInteractiveServerComponents();


      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddMemoryCache();




      builder.Services.AddSingleton<IDbConnection, DbConnection>();
      builder.Services.AddSingleton<IAssetData, MongoAssetData>();
      builder.Services.AddSingleton<IAssetRequestData, MongoAssetRequestData>();
      builder.Services.AddSingleton<IUserData, MongoUserData>();

   }

}
