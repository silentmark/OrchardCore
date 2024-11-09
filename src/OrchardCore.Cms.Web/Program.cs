using Markdig;
using OrchardCore.Logging;
using OrchardCore.Markdown.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLogHost();

builder.Services
    .AddOrchardCms()
    .ConfigureServices(tenantServices =>
    {
        tenantServices.PostConfigure<MarkdownPipelineOptions>(o =>
        {
            o.Configure.Clear();
        });
        tenantServices.ConfigureMarkdownPipeline((pipeline) =>
        {
            pipeline.UseAdvancedExtensions();
        });
    })
    .AddSetupFeatures("OrchardCore.AutoSetup");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseOrchardCore();

await app.RunAsync();
