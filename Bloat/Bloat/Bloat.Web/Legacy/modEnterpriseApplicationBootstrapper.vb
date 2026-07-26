'
' $Id: modEnterpriseApplicationBootstrapper.vb,v 1.17 2004/11/09 02:14:51 svc_build Exp $
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
                Return Results.Content(
                    AmplificationRequestPage.Render(),
                    "text/html; charset=utf-8")
            End Function)

    End Sub

End Module