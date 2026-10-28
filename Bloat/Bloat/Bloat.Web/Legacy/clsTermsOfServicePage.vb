'
' $Id: clsTermsOfServicePage.vb,v 1.0 2003/11/14 16:59:02 legal_department Exp $
' $Source: /CorporateSystems/BLOAT/Web/App_Code/Legal/TermsOfService.aspx.vb $
'
' CORPORATE LEGAL SERVICES DIVISION
' DOCUMENT CONTROL: BLT-LEGAL-0001
'
' IMPORTANT:
' These terms were approved by the Corporate Legal Affairs Committee
' during an emergency meeting held on November 14, 2003.
'
' Meeting minutes indicate that no committee members were present.
' Approval was nevertheless recorded unanimously.
'
' REVISION HISTORY:
'   1.0 - Initial terms drafted by outside counsel.
'   1.1 - Outside counsel requested payment.
'   1.2 - Outside counsel no longer responds to correspondence.
'   1.3 - Legal review responsibilities transferred to Dennis.
'   1.4 - Dennis no longer works here.
'
' COMPLIANCE NOTICE:
' Removing this file may result in disciplinary action by a department
' that has not appeared on the organizational chart since 2007.
'
' DO NOT MODIFY WITHOUT WRITTEN APPROVAL.
' The approval form has been discontinued.
'

Public NotInheritable Class TermsOfServicePage

    Private Sub New()
    End Sub

    Public Shared Function Render() As String

        Dim document =
            <html lang="en">
                <head>
                    <meta charset="utf-8"/>
                    <meta name="viewport" content="width=device-width, initial-scale=1"/>
                    <title>B.L.O.A.T. - Terms of Service</title>
                    <link rel="stylesheet" href="/css/legacy-enterprise.css"/>
                </head>

                <body>
                    <div class="enterprise-window">
                        <div class="title-bar">
                            <span>B.L.O.A.T. Office of Legal Affairs</span>

                            <div class="title-bar-controls" aria-hidden="true">
                                <span class="title-bar-button">_</span>
                                <span class="title-bar-button">□</span>
                                <span class="title-bar-button">×</span>
                            </div>
                        </div>

                        <header class="enterprise-header">
                            <h1>Terms of Service</h1>

                            <p class="expanded-name">
                                Bureaucratic Link Obfuscation &amp; Amplification Technology
                            </p>

                            <p class="tagline">
                                Mandatory Administrative Disclosures
                            </p>
                        </header>

                        <div class="system-metadata">
                            <div class="metadata-item">DOCUMENT: BLT-LEGAL-0001</div>
                            <div class="metadata-item">REVISION: 1.0</div>
                            <div class="metadata-item">CLASSIFICATION: PUBLIC</div>
                        </div>

                        <main class="workspace">
                            <section class="panel">
                                <h2 class="panel-title">
                                    Terms, Conditions, and Other Unavoidable Formalities
                                </h2>

                                <div class="panel-content">

                                    <h3>1. Description of Service</h3>

                                    <p>
                                        B.L.O.A.T. is a novelty service that transforms
                                        ordinary URLs into unnecessarily complicated
                                        navigation experiences.
                                    </p>

                                    <p>
                                        Additional steps, delays, acknowledgements,
                                        and administrative obstacles are intentional.
                                    </p>

                                    <h3>2. Acceptable Use</h3>

                                    <p>
                                        You may use this service to create links to lawful
                                        web resources. You must not use B.L.O.A.T. for:
                                    </p>

                                    <ul>
                                        <li>Phishing, fraud, scams, or impersonation.</li>
                                        <li>Distributing malware or other harmful content.</li>
                                        <li>Harassment or unlawful activities.</li>
                                        <li>Spam or abusive automated submissions.</li>
                                        <li>Manipulating the abuse-reporting system.</li>
                                    </ul>

                                    <p>
                                        Violations may result in link removal or
                                        restrictions imposed by the Department of
                                        Things We Would Rather Not Deal With.
                                    </p>

                                    <h3>3. External Websites</h3>

                                    <p>
                                        B.L.O.A.T. does not own, host, control, or endorse
                                        the websites referenced by submitted links.
                                    </p>

                                    <p>
                                        Administrative approval, case registration,
                                        or successful navigation does not constitute
                                        a security certification.
                                    </p>

                                    <h3>4. Abuse Reports and Moderation</h3>

                                    <p>
                                        Where reporting functionality is available,
                                        users may report potentially abusive links.
                                        Reports are allegations, not verified findings.
                                    </p>

                                    <p>
                                        B.L.O.A.T. may investigate reports and disable
                                        links at its discretion. Submission of a report
                                        does not guarantee action.
                                    </p>

                                    <p>
                                        All complaints receive the highest priority
                                        permitted by current staffing levels.
                                    </p>

                                    <h3>5. Expiration and Availability</h3>

                                    <p>
                                        Generated links may expire, become unavailable,
                                        or be disabled without prior notice.
                                    </p>

                                    <p>
                                        Permanent storage, uninterrupted availability,
                                        and successful navigation are not guaranteed.
                                    </p>

                                    <p>
                                        Applicants requiring reliable access are advised
                                        to retain the original URL.
                                    </p>

                                    <h3>6. Security and Warranties</h3>

                                    <p>
                                        The service is provided for entertainment and
                                        experimentation, as-is and as available, without
                                        warranties to the extent permitted by law.
                                    </p>

                                    <p>
                                        B.L.O.A.T. does not guarantee that submitted
                                        destinations are safe. Users access external
                                        websites at their own risk.
                                    </p>

                                    <h3>7. Changes to These Terms</h3>

                                    <p>
                                        These terms may be updated as the service evolves.
                                        The current version will be published here.
                                    </p>

                                    <p>
                                        Changes do not require approval from the
                                        Interdepartmental Hypertext Governance Committee,
                                        as no evidence has been found that the committee
                                        still exists.
                                    </p>

                                    <aside class="administrative-notice">
                                        <strong>FINAL ADMINISTRATIVE NOTICE</strong>

                                        Your review of these terms has been noted,
                                        although no acknowledgement was requested.
                                        Further correspondence should reference
                                        Form BLT-LEGAL-0001.
                                    </aside>

                                    <p>
                                        <a href="/">RETURN TO INTAKE PORTAL</a>
                                    </p>

                                </div>
                            </section>
                        </main>

                        <footer class="enterprise-footer">
                            <div class="status-bar">
                                <div class="status-bar-item">
                                    LEGAL REVIEW COMPLETE
                                </div>

                                <div class="status-bar-item">
                                    Dennis no longer works here.
                                </div>
                            </div>
                        </footer>
                    </div>
                </body>
            </html>

        Return String.Concat("<!DOCTYPE html>", Environment.NewLine, document.ToString())

    End Function

End Class