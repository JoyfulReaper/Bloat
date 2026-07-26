'
' $Id: clsAmplificationCaseRegistryPage.vb,v 2.3 2006/08/14 18:03:22 webforms_vendor Exp $
' $Source: /CorporateSystems/BLOAT/Web/App_Code/Cases/CaseRegistry.aspx.vb $
'
' RECORDS MANAGEMENT NOTICE:
' This page is the authoritative display of a non-authoritative record.
'
' TODO:
' Add clipboard functionality after JavaScript procurement is approved.
'
' SECURITY:
' The credentials previously stored below were replaced with a reference
' to a sealed envelope that could not subsequently be located.
'

Imports Bloat.Core.Amplification

Public NotInheritable Class AmplificationCaseRegistryPage

    Private Sub New()
    End Sub

    Public Shared Function Render(amplificationCase As AmplificationCase, amplifiedUrl As String) As String

        Dim document =
            <html lang="en">
                <head>
                    <meta charset="utf-8"/>
                    <meta
                        name="viewport"
                        content="width=device-width, initial-scale=1"/>

                    <title>B.L.O.A.T. Case Registry</title>

                    <link
                        rel="stylesheet"
                        href="/css/legacy-enterprise.css"/>
                </head>

                <body>
                    <div class="enterprise-window">
                        <div class="title-bar">
                            <span>
                                B.L.O.A.T. Amplification Case Registry
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
                                External Resource Case Administration
                            </p>
                        </header>

                        <div class="system-metadata">
                            <div class="metadata-item">
                                CASE:
                                <%= amplificationCase.CaseNumber %>
                            </div>

                            <div class="metadata-item">
                                FORM: BLT-ACR-0041
                            </div>

                            <div class="metadata-item">
                                RETENTION: VOLATILE
                            </div>
                        </div>

                        <main class="workspace">
                            <section class="panel">
                                <h2 class="panel-title">
                                    Amplification Case Registry Record
                                </h2>

                                <div class="panel-content">
                                    <div class="status-box">
                                        <span class="status-label">
                                            CASE STATUS:
                                        </span>

                                        <span class="status-value">
                                            OPEN — TRANSFER AUTHORIZATION PENDING
                                        </span>
                                    </div>

                                    <div class="form-row">
                                        <label for="caseNumber">
                                            Administrative Case Number
                                        </label>

                                        <input
                                            id="caseNumber"
                                            type="text"
                                            value=<%= amplificationCase.CaseNumber %>
                                            readonly="readonly"/>
                                    </div>

                                    <div class="form-row">
                                        <label for="originalUrl">
                                            Underlying Destination URL
                                        </label>

                                        <input
                                            id="originalUrl"
                                            type="url"
                                            value=<%= amplificationCase.OriginalUrl %>
                                            readonly="readonly"/>
                                    </div>

                                    <div class="form-row">
                                        <label for="amplifiedUrl">
                                            Amplified Resource Locator
                                        </label>

                                        <input
                                            id="amplifiedUrl"
                                            type="url"
                                            value=<%= amplifiedUrl %>
                                            readonly="readonly"/>

                                        <p class="form-help">
                                            Clipboard automation remains pending
                                            procurement. Select the field and copy
                                            it manually.
                                        </p>

                                        <p>
                                            <a href=<%= amplifiedUrl %>>
                                                OPEN AMPLIFIED RESOURCE LOCATOR
                                            </a>
                                        </p>
                                    </div>

                                    <div class="form-row">
                                        <label>
                                            Case Opened
                                        </label>

                                        <input
                                            type="text"
                                            value=<%= amplificationCase.CreatedAtUtc.ToString("u") %>
                                            readonly="readonly"/>
                                    </div>

                                    <aside class="administrative-notice">
                                        <strong>
                                            VOLATILE RECORDS DISCLOSURE
                                        </strong>

                                        This record is retained exclusively in
                                        application memory. Restarting the system
                                        may result in immediate and comprehensive
                                        administrative amnesia.
                                    </aside>

                                    <div class="form-row">
                                        <button
                                            type="button"
                                            disabled="disabled">
                                            CONTINUE TO EXTERNAL RESOURCE
                                            — FORM BLT-ACK-07 REQUIRED
                                        </button>
                                    </div>

                                    <div class="form-row">
                                        <form method="get" action="/">
                                            <button type="submit">
                                                OPEN ANOTHER CASE
                                            </button>
                                        </form>
                                    </div>
                                </div>
                            </section>
                        </main>

                        <footer class="enterprise-footer">
                            <div class="status-bar">
                                <div class="status-bar-item">
                                    CASE REGISTERED —
                                    Powered by the Inconvenience Engine
                                </div>

                                <div class="status-bar-item">
                                    Useful navigation remains unavailable
                                </div>
                            </div>
                        </footer>
                    </div>
                </body>
            </html>

        Return String.Concat("<!DOCTYPE html>", Environment.NewLine, document.ToString())
    End Function

End Class