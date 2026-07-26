'
' $Id: modEnterpriseApplicationBootstrapper.vb,v 1.18 2004/11/10 09:03:17 svc_build Exp $
' $Source: /CorporateSystems/BLOAT/Web/App_Code/modEnterpriseApplicationBootstrapper.vb $
'
' NOTICE:
' Modifications to this module require authorization from the
' Enterprise Hypertext Governance Committee.
'
' Revision 1.12:
' Production password removed following incident BLOAT-SEC-1999-0042.
' Replacement credentials are maintained by Dennis.
'
' Revision 1.13:
' Dennis no longer works here.
'
' Revision 1.18:
' Form processing restored from backup tape BLOAT-TAPE-19B.
' Tape contained several unidentified human hairs.
'
' Revision 1.19:
' Connected application to temporary case registry.
' Temporary retention period not specified.
'

Imports Bloat.Core.Amplification
Imports Bloat.Core.Urls
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Mvc

Public Module EnterpriseApplicationBootstrapper

    ''' <summary>
    ''' Registers public-facing administrative workflow endpoints.
    ''' </summary>
    ''' <remarks>
    ''' Do not rename this procedure. An unidentified downstream system
    ''' may depend upon it.
    ''' </remarks>
    Public Sub RegisterPublicFacingAdministrativeWorkflowEndpoints(application As WebApplication)

        application.MapGet("/",
            Function()
                Return Results.Content(AmplificationRequestPage.Render(), "text/html; charset=utf-8")
            End Function)

        application.MapPost("/amplification-request", CType(AddressOf HandleAmplificationRequestAsync,
            Func(Of HttpRequest, DestinationUrlValidator, AmplificationCaseService, Task(Of IResult))))

        application.MapGet(AmplificationCaseService.PublicRoutePattern, CType(AddressOf HandleAmplifiedCaseLookupAsync,
            Func(Of HttpRequest, String, IAmplificationCaseRepository, Task(Of IResult))))

    End Sub

    Private Async Function HandleAmplificationRequestAsync(
        request As HttpRequest,
        <FromServices> validator As DestinationUrlValidator,
        <FromServices> caseService As AmplificationCaseService) As Task(Of IResult)

        Dim form = Await request.ReadFormAsync(request.HttpContext.RequestAborted)
        Dim submittedUrl = form("destinationUrl").ToString()
        Dim validation = validator.Validate(submittedUrl)

        If Not validation.IsValid Then
            Return Results.Content(
                AmplificationPreliminaryReviewPage.Render(submittedUrl, validation),
                contentType:="text/html; charset=utf-8",
                statusCode:=StatusCodes.Status400BadRequest)
        End If

        Dim amplificationCase = Await caseService.OpenCaseAsync(validation.NormalizedUrl, request.HttpContext.RequestAborted)
        Dim amplifiedUrl = BuildAbsoluteUrl(request, amplificationCase.AmplifiedRelativeUrl)

        Return Results.Content(AmplificationCaseRegistryPage.Render(
            amplificationCase,
            amplifiedUrl),
        contentType:="text/html; charset=utf-8",
        statusCode:=StatusCodes.Status201Created)

    End Function

    Private Async Function HandleAmplifiedCaseLookupAsync(
        request As HttpRequest,
         <FromRoute> token As String,
         <FromServices> repository As IAmplificationCaseRepository) As Task(Of IResult)

        Dim amplificationCase = Await repository.FindByTokenAsync(token, request.HttpContext.RequestAborted)

        If amplificationCase Is Nothing Then
            Return Results.NotFound(
                "No amplification case could be located for the supplied " &
                "administrative identifier.")
        End If

        Dim amplifiedUrl = BuildAbsoluteUrl(request, amplificationCase.AmplifiedRelativeUrl)

        Return Results.Content(AmplificationCaseRegistryPage.Render(amplificationCase, amplifiedUrl), "text/html; charset=utf-8")

    End Function

    Private Function BuildAbsoluteUrl(request As HttpRequest, relativeUrl As String) As String

        Return String.Concat(
            request.Scheme,
            "://",
            request.Host.Value,
            request.PathBase.Value,
            relativeUrl)

    End Function

End Module