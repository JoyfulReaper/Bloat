'
' $Id: clsAmplificationRequestPage.vb,v 3.6 2005/01/17 19:03:12 contractor_unknown Exp $
'
' TODO: Replace XML literals after migration to ASP.NET 2.0.
' TODO: Confirm whether ASP.NET 2.0 migration occurred.
' NOTE: Do not contact the original vendor. Contract dispute remains open.
'

Public NotInheritable Class AmplificationRequestPage

    Private Sub New()
    End Sub

    Public Shared Function Render() As String
        Dim document =
    <html lang="en">
        <head>
            <meta charset="utf-8"/>
            <meta name="viewport" content="width=device-width, initial-scale=1"/>
            <title>B.L.O.A.T. Enterprise Intake Portal</title>
            <link rel="stylesheet" href="/css/legacy-enterprise.css"/>
        </head>

        <body>
            <div class="enterprise-window">
                <div class="title-bar">
                    <span>B.L.O.A.T. Enterprise Intake Portal</span>

                    <div class="title-bar-controls" aria-hidden="true">
                        <span class="title-bar-button">_</span>
                        <span class="title-bar-button">□</span>
                        <span class="title-bar-button">×</span>
                    </div>
                </div>

                <header class="enterprise-header">
                    <h1>B.L.O.A.T.</h1>

                    <p class="expanded-name">
                        Bureaucratic Link Obfuscation &amp; Amplification Technology
                    </p>

                    <p class="tagline">
                        Restoring necessary friction, enterprise latency,
                        and protocol overhead to the modern web.
                    </p>
                </header>

                <div class="system-metadata">
                    <div class="metadata-item">
                        SYSTEM: BLOAT-PRD-01
                    </div>

                    <div class="metadata-item">
                        FORM: BLT-ERAR-0001
                    </div>

                    <div class="metadata-item">
                        CLASSIFICATION: PUBLIC/INTERNAL
                    </div>
                </div>

                <main class="workspace">
                    <section class="panel">
                        <h2 class="panel-title">
                            External Resource Amplification Request
                        </h2>

                        <div class="panel-content">
                            <div class="status-box">
                                <span class="status-label">REQUEST STATUS:</span>
                                <span class="status-value">
                                    AWAITING APPLICANT INPUT
                                </span>
                            </div>

                            <form method="post" action="/amplification-request">
                                <div class="form-row">
                                    <label for="destinationUrl">
                                        Underlying Destination URL
                                    </label>

                                    <input
                                        id="destinationUrl"
                                        name="destinationUrl"
                                        type="url"
                                        maxlength="2048"
                                        required="required"/>

                                    <p class="form-help">
                                        Enter the complete external hypertext
                                        resource locator, including protocol.
                                    </p>
                                </div>

                                <div class="form-row">
                                    <label for="amplificationLevel">
                                        Administrative Burden Classification
                                    </label>

                                    <select
                                        id="amplificationLevel"
                                        name="amplificationLevel">
                                        <option
                                            value="enterprise"
                                            selected="selected">
                                            Enterprise Procedure
                                        </option>
                                    </select>
                                </div>

                                <div class="form-row">
                                    <button type="submit">
                                        SUBMIT REQUEST FOR PROCESSING
                                    </button>
                                </div>
                            </form>

                            <aside class="administrative-notice">
                                <strong>NOTICE TO APPLICANTS</strong>

                                Submission of this form does not imply approval,
                                acceptance, processing, review, or awareness of
                                the underlying destination by any department.
                            </aside>
                        </div>
                    </section>
                </main>

                <footer class="enterprise-footer">
                    <div class="status-bar">
                        <div class="status-bar-item">
                            READY — Powered by the Inconvenience Engine
                        </div>

                        <div class="status-bar-item">
                            Build 0.0.0-PRE-PRODUCTION-RC0 |
                            Best viewed at 1024×768
                        </div>
                    </div>
                </footer>
            </div>
        </body>
    </html>

        Return String.Concat("<!DOCTYPE html>", Environment.NewLine, document.ToString())
    End Function

End Class