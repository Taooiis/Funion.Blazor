﻿namespace FunionBlazor.Web.Entry;

public sealed class MASAWebComponent : IWebComponent
{
    public void Load(WebApplicationBuilder builder, ComponentContext componentContext)
    {
        builder.WebHost.UseUrls("https://*:1126");
    }
}