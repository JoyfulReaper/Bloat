'
' $Id: clsExternalResourceTransferPage.vb,v 4.1 2007/03/18 04:17:52 legal_review Exp $
' $Source: /CorporateSystems/BLOAT/Web/App_Code/Transfer/ExternalResourceTransfer.aspx.vb $
'
' LEGAL NOTICE:
' This screen was approved for production use provided that no employee,
' contractor, visitor, or automated process interprets the word "approved"
' as indicating approval.
'
' SECURITY INCIDENT HISTORY:
' The destination hyperlink was removed in revision 3.8 after users
' discovered they could bypass the acknowledgement procedure by clicking it.
'

Imports Bloat.Core.Amplification

Public NotInheritable Class ExternalResourceTransferPage

    Private Sub New()
    End Sub

    Public Shared Function Render(
        amplificationCase As AmplificationCase,
        authorizationRelativeUrl As String,
        Optional failureMessage As String = Nothing) As String

        Dim destinationUri = New Uri(amplificationCase.OriginalUrl)
        Dim hasFailure = Not String.IsNullOrWhiteSpace(failureMessage)

        Dim statusText = If(hasFailure,
            "TRANSFER AUTHORIZATION INCOMPLETE",
            "AWAITING RECIPIENT ACKNOWLEDGEMENT")

        Dim noticeHeading = If(hasFailure,
            "PROCEDURAL IRREGULARITY",
            "EXTERNAL RESOURCE TRANSFER NOTICE")

        Dim noticeText = If(hasFailure, failureMessage,
            "The requested resource is located outside the B.L.O.A.T. " &
            "administrative boundary. Continued navigation requires " &
            "affirmative acknowledgement of this entirely obvious fact.")

        Dim document =
            <html lang="en">
                <head>
                    <meta charset="utf-8"/>
                    <meta name="viewport" content="width=device-width, initial-scale=1"/>
                    <title>B.L.O.A.T. External Resource Transfer</title>
                    <link rel="stylesheet" href="/css/legacy-enterprise.css"/>
                </head>

                <body>
                    <div class="enterprise-window">
                        <div class="title-bar">
                            <span>
                                B.L.O.A.T. External Resource Transfer Terminal
                            </span>

                            <div
                                class="title-bar-controls"
                                aria-hidden="true">
                                <span class="title-bar-button">_</span>
                                <span class="title-bar-button">□</span>
                                <span class="title-bar-button">×</span>
                            </div>
                        </div>

                        <header class="enterprise-header">
                            <h1>B.L.O.A.T.</h1>

                            <p class="expanded-name">
                                Bureaucratic Link Obfuscation &amp;
                                Amplification Technology
                            </p>

                            <p class="tagline">
                                Controlled External Hypertext Navigation
                            </p>
                        </header>

                        <div class="system-metadata">
                            <div class="metadata-item">
                                CASE:
                                <%= amplificationCase.CaseNumber %>
                            </div>

                            <div class="metadata-item">
                                FORM: BLT-ACK-0007
                            </div>

                            <div class="metadata-item">
                                AUTHORIZATION: PENDING
                            </div>
                        </div>

                        <main class="workspace">
                            <section class="panel">
                                <h2 class="panel-title">
                                    External Resource Transfer Authorization
                                </h2>

                                <div class="panel-content">
                                    <div class="status-box">
                                        <span class="status-label">
                                            TRANSFER STATUS:
                                        </span>

                                        <span class="status-value">
                                            <%= statusText %>
                                        </span>
                                    </div>

                                    <div class="form-row">
                                        <label for="destinationHost">
                                            Destination Host
                                        </label>

                                        <input
                                            id="destinationHost"
                                            type="text"
                                            value=<%= destinationUri.Host %>
                                            readonly="readonly"/>
                                    </div>

                                    <div class="form-row">
                                        <label for="destinationUrl">
                                            Underlying Destination URL
                                        </label>

                                        <input
                                            id="destinationUrl"
                                            type="url"
                                            value=<%= amplificationCase.OriginalUrl %>
                                            readonly="readonly"/>
                                    </div>

                                    <aside class="administrative-notice">
                                        <strong>
                                            <%= noticeHeading %>
                                        </strong>

                                        <%= noticeText %>
                                    </aside>

                                    <form
                                        method="post"
                                        action=<%= authorizationRelativeUrl %>>

                                        <div class="form-row">
                                            <label>
                                                <input
                                                    name="externalResourceAcknowledgement"
                                                    type="checkbox"
                                                    value="accepted"
                                                    required="required"/>

                                                I acknowledge that selecting
                                                “Continue” may cause my browser
                                                to navigate to the destination
                                                displayed immediately above.
                                            </label>
                                        </div>

                                        <div class="form-row">
                                            <button type="submit">
                                                ACKNOWLEDGE AND CONTINUE
                                                TO EXTERNAL RESOURCE
                                            </button>
                                        </div>
                                    </form>

                                    <div class="form-row">
                                        <form method="get" action="/">
                                            <button type="submit">
                                                ABANDON TRANSFER REQUEST
                                            </button>
                                        </form>
                                    </div>
                                </div>
                            </section>
                        </main>

                        <footer class="enterprise-footer">
                            <div class="status-bar">
                                <div class="status-bar-item">
                                    USER ACTION REQUIRED —
                                    Powered by the Inconvenience Engine
                                </div>

                                <div class="status-bar-item">
                                    Automatic convenience has been disabled
                                </div>
                            </div>
                        </footer>
                    </div>
                </body>
            </html>

        Return String.Concat("<!DOCTYPE html>", Environment.NewLine, document.ToString())
    End Function

End Class