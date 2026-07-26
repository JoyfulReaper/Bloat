# B.L.O.A.T.

## Bureaucratic Link Obfuscation & Amplification Technology

> Restoring necessary friction, enterprise latency, and protocol overhead to the modern web.

**Powered by the Inconvenience Engine.**

B.L.O.A.T. is the unnecessary alternative to a URL shortener. It accepts an
ordinary URL and transforms it into a needlessly long, bureaucratic resource
locator backed by an equally unnecessary transfer-authorization process.

Where conventional services optimize links for brevity and convenience,
B.L.O.A.T. restores the administrative burden the modern web has carelessly
removed.

## MVP status

The web MVP is implemented.

It currently provides:

- Intake and validation for absolute HTTP and HTTPS URLs
- Rejection of malformed URLs, unsupported schemes, embedded credentials,
  control characters, and inputs longer than 2,048 characters
- Cryptographically random case tokens and magnificently long public URLs
- A case registry showing the case number, destination, generated link, and
  opening time
- An intermediate transfer notice that clearly displays the destination host
  and full URL
- A mandatory acknowledgment before an HTTP redirect is issued
- An appropriately dated enterprise interface implemented in legacy-flavored
  VB.NET XML literals
- Automated tests for URL validation, case creation, and the transfer page

Cases are stored in memory. Restarting the application therefore causes
immediate and comprehensive administrative amnesia, and previously issued
links stop working.

## Example

Input:

```text
https://example.com/cats
```

Amplified output:

```text
http://localhost:5233/department/bureaucratic-link-processing/division/external-resource-amplification/office/provisional-hypertext-navigation/case/2c77d4c98e1c1df27ee729cd67831b8b0aa64b529ca796fa289148510aae6ed9?caseNumber=BLT-20260726-2C77D4C9&workflowPhase=preliminary-approval-complete&interdepartmentalRoutingStatus=pending&complianceReviewDisposition=no-objection-recorded&minimumRequiredFriction=restored
```

Opening that link presents an external-resource transfer notice. The recipient
must review the displayed destination and affirmatively acknowledge the obvious
before B.L.O.A.T. reluctantly redirects them.

## Run it locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

From the repository root:

```powershell
dotnet run --project Bloat/Bloat/Bloat.Host
```

Then open [http://localhost:5233](http://localhost:5233). The development launch
profile also opens a browser automatically.

To run the test suite:

```powershell
dotnet test Bloat/Bloat/Bloat.slnx
```

## Administrative workflow

1. Submit an underlying destination URL.
2. Await preliminary eligibility review.
3. Receive an amplification case number and public resource locator.
4. Share the amplified locator with an appropriately inconvenienced recipient.
5. Require that recipient to inspect the destination and complete Form
   BLT-ACK-0007.
6. Permit ordinary browser navigation only after acknowledgment is recorded.

## Architecture

The solution targets .NET 10 and separates its responsibilities with all the
ceremony the task deserves:

| Project | Administrative responsibility |
| --- | --- |
| `Bloat.Host` | ASP.NET Core host, dependency registration, and static assets |
| `Bloat.Core` | URL validation, case records, and amplification policy |
| `Bloat.Data` | Concurrent in-memory case registry |
| `Bloat.Web` | VB.NET endpoint registration and server-rendered enterprise UI |
| `Bloat.Tests` | NUnit coverage for core behavior and rendered workflow pages |

The application does not fetch destination content. It records the submitted
URL, displays it to the recipient, and returns an HTTP redirect only after the
required acknowledgment.

## Current limitations

- Case records are not persisted across application restarts.
- There is one approved burden classification: **Enterprise Procedure**.
- Clipboard automation remains pending procurement; generated links must be
  copied manually.
- Authentication, moderation, abuse reporting, disabling links, and rate
  limiting are not yet implemented.
- Deployment and production hardening remain outside the MVP.

## Grand design: cross-protocol retrieval bureaucracy

The planned destination of B.L.O.A.T. is not merely a longer URL. It is a
protocol obstacle course in which each service discloses only enough information
to inconvenience the applicant at the next stage.

```text
Submit normal URL
        ↓
Receive absurdly long HTTP URL
        ↓
Initial Administrative Referral
HTTP withholds the destination and issues a Gopher URL
        ↓
Legacy Transport Instruction Document
Gopher provides hexadecimal bytes and Echo instructions
        ↓
Binary Round-Trip Confirmation Procedure
Send bytes to echo.kgivler.com:7 and receive them unchanged
        ↓
Decode the returned bytes as UTF-8
        ↓
Final Destination Disclosure Service
Finger looks up the resulting token and finally reveals the URL
```

The protocol stages have been assigned the following departmental names:

| Protocol | Administrative function |
| --- | --- |
| HTTP | Initial Administrative Referral |
| Gopher | Legacy Transport Instruction Document |
| Echo | Binary Round-Trip Confirmation Procedure |
| Finger | Final Destination Disclosure Service |

### One token, several departments

A single fixed-length token can drive the entire procedure:

```text
Token → Original URL
```

For example:

```text
42fd9e6b57384060a367e93e950283094ea4dbe7a467496e930f5bb68d021eed
```

The Gopher document renders the UTF-8 bytes of that token as hexadecimal:

```text
34 32 66 64 39 65 36 62 35 37 33 38 34 30 36 30
61 33 36 37 65 39 33 65 39 35 30 32 38 33 30 39
34 65 61 34 64 62 65 37 61 34 36 37 34 39 36 65
39 33 30 66 35 62 62 36 38 64 30 32 31 65 65 64
```

Echo returns those bytes unchanged. Decoding them as UTF-8 yields the original
64-character token, which becomes:

```console
finger 42fd9e6b57384060a367e93e950283094ea4dbe7a467496e930f5bb68d021eed@finger.kgivler.com
```

Finger can then use the token to retrieve the original URL:

```text
Login: 42fd9e6b57384060a367e93e950283094ea4dbe7a467496e930f5bb68d021eed
Name: External Hypertext Resource Disclosure Record
Directory: /world/wide/web
Shell: /usr/bin/curl

Following completion of the approved retrieval procedure,
the requested destination is:

https://example.com/cats

B.L.O.A.T. thanks you for tolerating the process.
```

No separate Echo challenge model is required initially. A sufficiently
insubordinate user can bypass Echo by decoding the Gopher bytes directly; that
is acceptable because Echo provides ceremonial friction, not security.

Maximum Administrative Burden may eventually require the Echo service to record
an approved round trip before Finger releases the destination. This would make
the useless step technically mandatory, which is the natural endpoint of
enterprise governance.

### Planned filings

- Persistent case storage and case revocation
- Gopher, Echo, and Finger services sharing the case-token registry
- Rate limiting and abuse-reporting controls
- Additional amplification levels, including Maximum Administrative Burden
- Unnecessary progress indicators and expanded acknowledgment procedures
- Optional proof that the ceremonial Echo round trip actually occurred

These items describe the intended direction, not the current MVP. No department
should interpret their appearance in this document as approval, scheduling,
funding, awareness, or acceptance of responsibility.

## Security posture

B.L.O.A.T. is intentionally inconvenient, but it should not be deceptive.

The MVP:

- Accepts only absolute HTTP and HTTPS destinations
- Rejects URLs containing embedded usernames or passwords
- Shows the destination host and full URL before navigation
- Never redirects automatically when an amplified link is opened
- Does not retrieve arbitrary destination content on the server
- Uses 256-bit random public case tokens

The current in-memory registry is suitable for demonstration and local
development, not production deployment.

## Philosophy

The modern web has become dangerously convenient.

Links are too short. Redirects are too fast. Users are rarely asked to
acknowledge a case number, review a transfer notice, or wait for an unnecessary
administrative determination.

B.L.O.A.T. intends to correct this market failure.

## Emotional impact assessment

B.L.O.A.T. feels like a joke that accidentally discovered a legitimate systems
architecture. It is playful, committed to its premise, and just plausible
enough to be dangerous. Every additional protocol makes the design more
technically coherent and less reasonable to use—the exact combination that
makes the project delightful.

It is no longer merely a URL lengthener. It is a **cross-protocol URL retrieval
bureaucracy**, which is much more distinctive and much stupider in exactly the
right way.

## License

B.L.O.A.T. is available under the [MIT License](LICENSE).

---

**B.L.O.A.T.**

*Efficiency is not guaranteed or intended.*
