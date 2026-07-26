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

Imports Bloat.Core.Urls
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Http

Public Module EnterpriseApplicationBootstrapper

    ''' <summary>
    ''' Registers public-facing administrative workflow endpoints.
    ''' </summary>
    ''' <remarks>
    ''' Do not rename this procedure. An unidentified downstream system
    ''' may depend upon it.
    ''' </remarks>
    Public Sub RegisterPublicFacingAdministrativeWorkflowEndpoints(
        application As WebApplication)

        application.MapGet(
            "/",
            Function()
                Return Results.Content(AmplificationRequestPage.Render(), "text/html; charset=utf-8")
            End Function)

        application.MapPost("/amplification-request",
            CType(AddressOf HandleAmplificationRequestAsync,
                Func(
                    Of HttpRequest,
                    DestinationUrlValidator,
                    Task(Of IResult))))

    End Sub

    Private Async Function HandleAmplificationRequestAsync(request As HttpRequest, validator As DestinationUrlValidator) As Task(Of IResult)

        Dim form = Await request.ReadFormAsync(request.HttpContext.RequestAborted)
        Dim submittedUrl = form("destinationUrl").ToString()
        Dim validation = validator.Validate(submittedUrl)

        Dim responseStatus = If(
            validation.IsValid,
            StatusCodes.Status200OK,
            StatusCodes.Status400BadRequest)

        Return Results.Content(
            AmplificationPreliminaryReviewPage.Render(submittedUrl, validation),
            contentType:="text/html; charset=utf-8",
            statusCode:=responseStatus)

    End Function

End Module