'
' $Id: clsAmplificationPreliminaryReviewPage.vb,v 1.8 2005/06/03 17:41:02 contractor_unknown Exp $
' $Source: /CorporateSystems/BLOAT/Web/App_Code/Review/PreliminaryReview.aspx.vb $
'
' CHANGE CONTROL:
' This screen was approved by the User Experience Steering Committee
' after eleven months of review.
'
' NOTE:
' The green approval indicator was removed because Purchasing had not
' selected an approved shade of green.
'

Imports Bloat.Core.Urls

Public NotInheritable Class AmplificationPreliminaryReviewPage

    Private Sub New()
    End Sub

    Public Shared Function Render(
        submittedUrl As String,
        validation As DestinationUrlValidationResult) As String

        Dim statusText As String
        Dim reviewMessage As String
        Dim displayedUrl As String

        If validation.IsValid Then
            statusText = "PRELIMINARY REVIEW APPROVED"
            reviewMessage =
                "The submitted destination is eligible to proceed to the " &
                "amplification workflow. No case has been opened and no useful " &
                "work has yet occurred."

            displayedUrl = validation.NormalizedUrl
        Else
            statusText = "PRELIMINARY REVIEW REJECTED"
            reviewMessage = validation.FailureReason
            displayedUrl = submittedUrl
        End If

        Dim document =
            <html lang="en">
                <head>
                    <meta charset="utf-8"/>
                    <meta
                        name="viewport"
                        content="width=device-width, initial-scale=1"/>

                    <title>B.L.O.A.T. Preliminary Review</title>

                    <link
                        rel="stylesheet"
                        href="/css/legacy-enterprise.css"/>
                </head>

                <body>
                    <div class="enterprise-window">
                        <div class="title-bar">
                            <span>B.L.O.A.T. Preliminary Review Terminal</span>

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
                                Preliminary External Resource Eligibility Review
                            </p>
                        </header>

                        <div class="system-metadata">
                            <div class="metadata-item">
                                SYSTEM: BLOAT-PRD-01
                            </div>

                            <div class="metadata-item">
                                FORM: BLT-PDER-0003
                            </div>

                            <div class="metadata-item">
                                REVIEW LEVEL: PRELIMINARY
                            </div>
                        </div>

                        <main class="workspace">
                            <section class="panel">
                                <h2 class="panel-title">
                                    Destination Eligibility Determination
                                </h2>

                                <div class="panel-content">
                                    <div class="status-box">
                                        <span class="status-label">
                                            REVIEW STATUS:
                                        </span>

                                        <span class="status-value">
                                            <%= statusText %>
                                        </span>
                                    </div>

                                    <div class="form-row">
                                        <label for="reviewedUrl">
                                            Reviewed Destination URL
                                        </label>

                                        <input
                                            id="reviewedUrl"
                                            type="url"
                                            value=<%= displayedUrl %>
                                            readonly="readonly"/>
                                    </div>

                                    <aside class="administrative-notice">
                                        <strong>
                                            PRELIMINARY DETERMINATION
                                        </strong>

                                        <%= reviewMessage %>
                                    </aside>

                                    <div class="form-row">
                                        <form method="get" action="/">
                                            <button type="submit">
                                                RETURN TO INTAKE FORM
                                            </button>
                                        </form>
                                    </div>
                                </div>
                            </section>
                        </main>

                        <footer class="enterprise-footer">
                            <div class="status-bar">
                                <div class="status-bar-item">
                                    REVIEW COMPLETE —
                                    Powered by the Inconvenience Engine
                                </div>

                                <div class="status-bar-item">
                                    Decision not binding upon any department
                                </div>
                            </div>
                        </footer>
                    </div>
                </body>
            </html>

        Return String.Concat(
            "<!DOCTYPE html>",
            Environment.NewLine,
            document.ToString())
    End Function

End Class