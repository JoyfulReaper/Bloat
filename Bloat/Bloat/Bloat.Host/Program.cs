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

using Bloat.Core.Urls;
using Bloat.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DestinationUrlValidator>();

var app = builder.Build();

app.UseStaticFiles();

EnterpriseApplicationBootstrapper.RegisterPublicFacingAdministrativeWorkflowEndpoints(app);

app.Run();