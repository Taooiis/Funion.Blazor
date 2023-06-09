namespace FunionBlazor.Web.Entry;

public sealed class MASAWebComponent : IWebComponent
{
    public void Load(WebApplicationBuilder builder, ComponentContext componentContext)
    {
        //builder.WebHost.UseUrls("http://localhost:5126",  "http://*:5126", "http://localhost:5127", "http://*:5127");
        //builder.Services.AddSingleton(sp =>
        //{
        //    //var navigationManager = sp.GetRequiredService<NavigationManager>();
        //    return new HubConnectionBuilder()
        //        .WithUrl("https://localhost:1126/hubs/chathub")
        //        .Build();
        //});
    }
}
