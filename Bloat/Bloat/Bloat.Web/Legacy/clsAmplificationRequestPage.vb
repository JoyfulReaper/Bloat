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
                </head>
                <body>
                    <header>
                        <h1>B.L.O.A.T.</h1>
                        <p>
                            <strong>
                                Bureaucratic Link Obfuscation &amp; Amplification Technology
                            </strong>
                        </p>
                        <p>
                            Restoring necessary friction, enterprise latency,
                            and protocol overhead to the modern web.
                        </p>
                    </header>

                    <hr/>

                    <main>
                        <h2>External Resource Amplification Request</h2>

                        <p>
                            Request Status:
                            <strong>AWAITING APPLICANT INPUT</strong>
                        </p>

                        <form method="post" action="/amplification-request">
                            <p>
                                <label for="destinationUrl">
                                    Underlying Destination URL
                                </label>
                            </p>

                            <p>
                                <input
                                    id="destinationUrl"
                                    name="destinationUrl"
                                    type="url"
                                    size="80"
                                    required="required"/>
                            </p>

                            <p>
                                <label for="amplificationLevel">
                                    Administrative Burden Classification
                                </label>
                            </p>

                            <p>
                                <select
                                    id="amplificationLevel"
                                    name="amplificationLevel"
                                    disabled="disabled">
                                    <option selected="selected">
                                        Enterprise Procedure
                                    </option>
                                </select>
                            </p>

                            <p>
                                <button type="submit" disabled="disabled">
                                    SUBMIT REQUEST FOR PROCESSING
                                </button>
                            </p>
                        </form>
                    </main>

                    <hr/>

                    <footer>
                        <p>Powered by the Inconvenience Engine.</p>
                        <small>
                            Build 0.0.0-PRE-PRODUCTION-RC0 |
                            Best viewed at 1024×768
                        </small>
                    </footer>
                </body>
            </html>

        Return String.Concat("<!DOCTYPE html>", Environment.NewLine, document.ToString())
    End Function

End Class