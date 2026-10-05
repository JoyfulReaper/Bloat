# B.L.O.A.T.

## Bureaucratic Link Obfuscation & Amplification Technology

> Restoring necessary friction, enterprise latency, and protocol overhead to the modern web.

**Powered by the Inconvenience Engine.**

B.L.O.A.T. is the unnecessary alternative to a URL shortener.

It accepts an ordinary HTTP or HTTPS URL and transforms it into a needlessly long,
bureaucratic resource locator backed by an equally unnecessary administrative
workflow.

Where conventional services optimize links for brevity and convenience, B.L.O.A.T.
restores the procedural burden the modern web has carelessly removed.

## Example

Input:

```text
https://example.com/cats
```

B.L.O.A.T. creates an amplification case and returns something resembling:

```text
https://bloat.kgivler.com/department/bureaucratic-link-processing/division/external-resource-amplification/office/provisional-hypertext-navigation/case/...
```

The resulting URL includes an unnecessarily substantial case identifier and
procedural metadata.

Opening it does **not** immediately redirect to the destination. The user is first
presented with the appropriate external-resource transfer paperwork and must
complete the required administrative procedure before navigation is authorized.

## Current Status

B.L.O.A.T. currently has a working basic web amplification workflow.

The application can:

- Validate submitted destination URLs.
- Accept absolute HTTP and HTTPS destinations.
- Create an amplification case.
- Generate a cryptographically random case token.
- Assign a bureaucratically appropriate case number.
- Produce an unnecessarily long public URL.
- Retrieve an existing case by token.
- Display the underlying destination before navigation.
- Require explicit authorization before continuing.
- Complete the external-resource transfer workflow.

Case storage is currently **in-memory only**. Restarting the application therefore
constitutes a complete records-retention event and removes previously issued cases.

Persistent storage is planned.

## Architecture

B.L.O.A.T. is a .NET application split into several deliberately respectable
enterprise components.

### `Bloat.Core`

Contains the application/domain logic, including:

- Destination URL validation.
- Amplification case models.
- Amplification case creation.
- Token and case-number generation.
- Public amplified-route construction.
- Repository abstractions.

The core layer does not depend on a particular persistence implementation.

### `Bloat.Data`

Contains persistence implementations for amplification cases.

The current implementation is:

```text
InMemoryAmplificationCaseRepository
```

Cases are stored in a concurrent in-memory dictionary keyed by their generated
token.

This is intentionally temporary infrastructure and means amplified links do not
survive an application restart.

### `Bloat.Web`

Contains the public-facing administrative workflow.

In accordance with applicable enterprise mandates, portions of the web workflow
are implemented in VB.NET and presented with appropriately legacy-enterprise
styling.

The workflow includes the request, preliminary-review, case-registry, and
external-resource-transfer pages.

### `Bloat.Host`

The ASP.NET Core host.

It configures dependency injection, static files, the current case repository,
and delegates endpoint registration to the enterprise application bootstrapper.

### `Bloat.Tests`

Contains automated coverage for core URL validation, amplification-case behavior,
and portions of the public web workflow.

## Amplification Cases

Each approved amplification request produces an `AmplificationCase` containing:

- A cryptographically random token.
- A human-readable B.L.O.A.T. case number.
- The original destination URL.
- The amplified relative URL.
- The UTC creation timestamp.

Case numbers follow the general form:

```text
BLT-YYYYMMDD-XXXXXXXX
```

Public amplified URLs are deliberately routed through:

```text
/department/bureaucratic-link-processing
/division/external-resource-amplification
/office/provisional-hypertext-navigation
/case/{token}
```

Additional query parameters record important administrative facts such as
workflow phase, routing status, compliance disposition, and whether the minimum
required level of friction has been restored.

## Amplification Levels

The long-term design allows for multiple levels of unnecessary procedure.

### Standard Bureaucracy

Adds a respectable amount of procedural language without making the link
completely unusable.

### Enterprise Procedure

Adds departmental routing, approval terminology, case identifiers, compliance
metadata, and other normal consequences of organizational maturity.

### Maximum Administrative Burden

For situations where ordinary inefficiency is insufficient.

This level is intended to support substantially more elaborate transfer
procedures, potentially including cross-protocol administrative requirements.

Not all amplification levels are implemented yet.

## The Inconvenience Engine

The Inconvenience Engine is the conceptual core responsible for applying
amplification policy to otherwise functional URLs.

Its responsibilities include or may eventually include:

- Generating bureaucratic path segments.
- Assigning case and request identifiers.
- Adding harmless procedural metadata.
- Enforcing acknowledgment requirements.
- Introducing measured administrative friction.
- Selecting amplification policies.
- Coordinating unnecessarily complicated transfer procedures.
- Ensuring that efficiency remains neither guaranteed nor intended.

## Future Possibilities

Potential future work includes:

- Persistent case storage.
- Case disabling and revocation.
- Multiple amplification/burden levels.
- Additional acknowledgment and approval stages.
- More extensive case-history and administrative metadata.
- Gopher-based transfer procedures.
- Finger-based status or lookup procedures.
- Echo-based authorization steps.
- Other obsolete, inappropriate, or deeply inconvenient Internet protocols.

Legacy protocol support is intentionally future work and is not required for the
normal web workflow.

## Security

B.L.O.A.T. is intentionally inconvenient, but it should not be deceptive.

The service is designed to:

- Accept only explicitly supported URL schemes.
- Clearly display the destination before navigation.
- Avoid automatic redirects when an amplified link is opened.
- Generate unpredictable public case tokens.
- Avoid fetching arbitrary destination content on the server.
- Allow abusive or malicious cases to be disabled in a future persistent model.
- Apply rate limiting to case creation before public deployment.

B.L.O.A.T. should make navigation annoying, not unsafe.

## Development

The solution currently contains:

```text
Bloat.Core
Bloat.Data
Bloat.Host
Bloat.Web
Bloat.Tests
```

Build and test with the .NET SDK:

```bash
dotnet build
dotnet test
```

Run the host project with:

```bash
dotnet run --project Bloat/Bloat/Bloat.Host
```

## Philosophy

The modern web has become dangerously convenient.

Links are too short.

Redirects are too fast.

Users are rarely assigned a case number, routed through the appropriate
department, presented with a compliance disposition, or required to acknowledge
that the minimum required level of friction has been restored.

B.L.O.A.T. intends to correct this market failure.

---

**B.L.O.A.T.**

*Efficiency is not guaranteed or intended.*
