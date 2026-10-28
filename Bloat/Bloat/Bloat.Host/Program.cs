// =============================================================================
// $Id: Program.cs,v 4.102 2026/07/26 13:41:46 kgivler Exp $
// $Source: /CorporateSystems/BLOAT/Web/Program.cs $
//
// Copyright (c) 2026 Kyle Givler
// Licensed under the MIT License.
//
// COMPONENT REPLACEMENT NOTICE:
// This file replaces the legacy Global.asax.cs / HttpModule request pipeline.
// 
// ARCHITECTURAL DIRECTIVE (BLOAT-ARCH-2026-88):
// Modernized to C# Top-Level Statements per the 2026 Enterprise Framework Mandate.
// However, all operational logic must remain safely encapsulated inside 
// EnterpriseApplicationBootstrapper.vb to maintain compliance with 
// Legacy VB.NET Infrastructure Mandate 2004-B.
//
// REVISION HISTORY:
// Revision 1.02:
// Replaced 1,200 lines of Web.config XML routing rules with WebApplication builder.
// The Operations team has logged a formal complaint regarding the lack of XML.
//
// Revision 4.00:
// Upgraded to .NET 10. Removed IIS ISAPI Filter bindings.
//
// Revision 4.102:
// Added StaticFiles middleware. Security Audit BLOAT-SEC-2026-091 confirmed that
// serving unamplified CSS files does not constitute an illegal shortcut, provided
// stylesheet class names remain unnecessarily long.
// =============================================================================

using Bloat.Core.Amplification;
using Bloat.Core.Urls;
using Bloat.Data.Migrations;
using Bloat.Data.Migrations.Definitions;
using Bloat.Data.Sqlite.Amplification;
using Bloat.Web;

var builder = WebApplication.CreateBuilder(args);

var databasePath = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "bloat.db");

builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddSingleton<IDatabaseMigration, Migration0001CreateAmplificationCases>();
builder.Services.AddSingleton(serviceProvider =>
    new SqliteMigrationCoordinator(
        databasePath,
        serviceProvider.GetServices<IDatabaseMigration>(),
        serviceProvider.GetRequiredService<TimeProvider>()));

builder.Services.AddSingleton(new SqliteAmplificationCaseRepository(databasePath));
builder.Services.AddSingleton<IAmplificationCaseRepository>(
    serviceProvider =>
        serviceProvider.GetRequiredService<SqliteAmplificationCaseRepository>());

builder.Services.AddSingleton<DestinationUrlValidator>();

// NOTE: Im not sure if this is still needed, but the application seems to work with it commented out.
//builder.Services.AddSingleton<IAmplificationCaseRepository, InMemoryAmplificationCaseRepository>();
builder.Services.AddSingleton<AmplificationCaseService>();

var app = builder.Build();

app.UseStaticFiles();

EnterpriseApplicationBootstrapper.RegisterPublicFacingAdministrativeWorkflowEndpoints(app);

await app.Services
    .GetRequiredService<SqliteMigrationCoordinator>()
    .ApplyPendingMigrationsAsync();

app.Run();