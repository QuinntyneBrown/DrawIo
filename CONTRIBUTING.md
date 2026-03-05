# Contributing to DrawIo.Cli

Thanks for your interest in contributing! This guide covers how to get started.

## Getting Started

1. Fork and clone the repository:

   ```bash
   git clone https://github.com/QuinntyneBrown/DrawIo.git
   cd DrawIo
   ```

2. Install prerequisites:

   - [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the CLI:

   ```bash
   dotnet run --project src/DrawIo.Cli -- install
   ```

## Making Changes

1. Create a branch from `main`:

   ```bash
   git checkout -b your-feature-name
   ```

2. Make your changes.

3. Test locally by running the install command and verifying the output in `~/.drawio/skills/`.

4. Commit your changes with a clear message describing what and why.

5. Push and open a pull request against `main`.

## Project Layout

| Path | Purpose |
|------|---------|
| `src/DrawIo.Cli/Commands/` | CLI command definitions (System.CommandLine) |
| `src/DrawIo.Cli/Services/` | Business logic behind the `ISkillInstaller` interface |
| `src/DrawIo.Cli/Skills/` | Skill definitions and embedded instructions |
| `src/DrawIo.Cli/Program.cs` | Entry point, dependency injection, hosting |
| `docs/specs/` | Requirements documents |
| `tests/` | Quality validation scripts and reports |

## Key Dependencies

- **Microsoft.Extensions.Hosting** — Dependency injection and hosting
- **System.CommandLine** — CLI argument parsing
- **Spectre.Console** — Terminal UI (spinners, colors, panels)

## Adding a New Skill

1. Create a new class in `src/DrawIo.Cli/Skills/` following the pattern in `DrawIoSkill.cs`.
2. Register it in `SkillInstaller.cs` so the installer can resolve it by name.
3. Test with `dotnet run --project src/DrawIo.Cli -- install your-skill-name`.

## Running Quality Tests

From the repository root:

```powershell
./tests/diagram-quality-test.ps1
```

This generates diagrams across all supported types and scores them against a 10-point quality rubric.

## Code Style

- Follow existing patterns in the codebase
- Use `async/await` for I/O operations
- Register new services through dependency injection in `Program.cs`

## Reporting Issues

Open an issue at [github.com/QuinntyneBrown/DrawIo/issues](https://github.com/QuinntyneBrown/DrawIo/issues).
