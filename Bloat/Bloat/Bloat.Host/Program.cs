using Bloat.Web;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

EnterpriseApplicationBootstrapper.RegisterPublicFacingAdministrativeWorkflowEndpoints(app);

app.Run();