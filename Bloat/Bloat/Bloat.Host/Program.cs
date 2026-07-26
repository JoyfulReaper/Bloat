using Bloat.Web;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

EnterpriseApplicationBootstrapper.RegisterPublicFacingAdministrativeWorkflowEndpoints(app);

app.Run();