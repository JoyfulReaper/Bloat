'
' $Id: clsAmplificationCaseExpiredPage.vb,v 3.14 2009/02/29 00:00:00 records_admin Exp $
' $Source: /CorporateSystems/BLOAT/Web/App_Code/Records/ExpiredCase.aspx.vb $
'
' RECORDS RETENTION AND DISPOSAL DIVISION
'
' ADMINISTRATIVE DIRECTIVE BLT-EXP-0030
'
' All amplification authorizations expire after thirty days.
'
' The expiration period was originally established at ninety days,
' reduced to sixty days, and subsequently reduced to thirty days
' following a departmental initiative to improve administrative
' efficiency by requiring applicants to submit more paperwork.
'
' IMPORTANT:
' Expired cases must not be reinstated.
'
' Appeals may be submitted using Form BLT-APPEAL-999.
' Form BLT-APPEAL-999 does not exist.
'
' Previous attempts to create an appeals process were rejected
' because successful appeals would undermine departmental authority.
'

Imports Bloat.Core.Amplification

Public NotInheritable Class AmplificationCaseExpiredPage

    Private Sub New()
    End Sub

    Public Shared Function Render(amplificationCase As AmplificationCase) As String

        ArgumentNullException.ThrowIfNull(amplificationCase)

        Dim expirationDate = AmplificationCaseExpiration.GetExpirationUtc(amplificationCase)

        Dim document =
            <html lang="en">
                <head>
                    <meta charset="utf-8"/>
                    <meta name="viewport" content="width=device-width, initial-scale=1"/>
                    <title>B.L.O.A.T. - Administrative Expiration Notice</title>
                    <link rel="stylesheet" href="/css/legacy-enterprise.css"/>
                </head>

                <body>
                    <div class="enterprise-window">
                        <div class="title-bar">
                            <span>B.L.O.A.T. Records Retention Division</span>

                            <div class="title-bar-controls" aria-hidden="true">
                                <span class="title-bar-button">_</span>
                                <span class="title-bar-button">□</span>
                                <span class="title-bar-button">×</span>
                            </div>
                        </div>

                        <header class="enterprise-header">
                            <h1>Authorization Expired</h1>

                            <p class="expanded-name">
                                Bureaucratic Link Obfuscation &amp; Amplification Technology
                            </p>

                            <p class="tagline">
                                Official Notification of Administrative Termination
                            </p>
                        </header>

                        <div class="system-metadata">
                            <div class="metadata-item">
                                CASE: <%= amplificationCase.CaseNumber %>
                            </div>

                            <div class="metadata-item">
                                DOCUMENT: BLT-EXP-0030
                            </div>

                            <div class="metadata-item">
                                STATUS: TERMINATED
                            </div>
                        </div>

                        <main class="workspace">
                            <section class="panel">
                                <h2 class="panel-title">
                                    Notice of Authorization Expiration
                                </h2>

                                <div class="panel-content">
                                    <div class="status-box">
                                        <span class="status-label">
                                            CASE STATUS:
                                        </span>

                                        <span class="status-value">
                                            EXPIRED — ACCESS DENIED
                                        </span>
                                    </div>

                                    <div class="form-row">
                                        <label>
                                            Administrative Expiration Date
                                        </label>

                                        <input
                                            type="text"
                                            value=<%= expirationDate.ToString("u") %>
                                            readonly="readonly"/>
                                    </div>

                                    <aside class="administrative-notice">
                                        <strong>OFFICIAL NOTICE</strong>

                                        Your authorization to navigate to the
                                        requested external hypertext resource
                                        has expired.

                                        Continued use of this case number is
                                        prohibited under Administrative
                                        Retention Directive BLT-EXP-0030.
                                    </aside>

                                    <p>
                                        To regain access to the requested resource,
                                        you must submit a new External Resource
                                        Amplification Request.
                                    </p>

                                    <p>
                                        Previous approvals, acknowledgements,
                                        and completed forms cannot be transferred
                                        to the new application.
                                    </p>

                                    <p>
                                        The department regrets any convenience
                                        that may have resulted from the original
                                        authorization.
                                    </p>

                                    <div class="form-row">
                                        <form method="get" action="/">
                                            <button type="submit">
                                                INITIATE NEW APPLICATION
                                            </button>
                                        </form>
                                    </div>
                                </div>
                            </section>
                        </main>

                        <footer class="enterprise-footer">
                            <div class="status-bar">
                                <div class="status-bar-item">
                                    HTTP 410 — ADMINISTRATIVELY GONE
                                </div>

                                <div class="status-bar-item">
                                    Your inconvenience is important to us.
                                </div>
                            </div>
                        </footer>
                    </div>
                </body>
            </html>

        Return String.Concat("<!DOCTYPE html>", Environment.NewLine, document.ToString())

    End Function

End Class