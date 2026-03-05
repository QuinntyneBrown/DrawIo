# DrawIo.Cli

A .NET global tool that installs professional Draw.io diagram-creation skills for AI agents. The installed skill provides comprehensive instructions covering XML structure, styling, layout, and templates for 10+ diagram types.

## Installation

```bash
dotnet tool install -g DrawIo.Cli
```

## Usage

```bash
drawio install
```

This installs the `drawio` skill definition to `~/.drawio/skills/drawio.json`. The skill includes:

- **10 diagram types** — flowcharts, UML class, sequence, ER, network/architecture, swimlanes, mind maps, org charts, C4 context, and more
- **Official color palette** with named themes (Corporate Blue, Warm Earth, Cool Contrast)
- **Typography hierarchy** — title, heading, body, annotation, and code styles
- **Complete XML templates** for every diagram type
- **Layout and composition rules** — spacing, grid alignment, connector routing
- **Anti-patterns** and common mistakes to avoid

## Quality

The skill has been validated across 400 generated diagrams with a **98.2/100 average quality score**. See [tests/quality-report.md](tests/quality-report.md) for details.

## Building from Source

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Build

```bash
dotnet build
```

### Run without installing

```bash
dotnet run --project src/DrawIo.Cli -- install
```

### Pack

```bash
dotnet pack src/DrawIo.Cli -c Release -o nupkg
```

### Install from local package

```bash
dotnet tool install -g DrawIo.Cli --add-source ./nupkg
```

## Project Structure

```
src/DrawIo.Cli/
  Commands/InstallCommand.cs    # CLI command definition
  Services/ISkillInstaller.cs   # Installer interface
  Services/SkillInstaller.cs    # Installs skill JSON to ~/.drawio/skills/
  Skills/DrawIoSkill.cs         # Skill definition and instructions
  Program.cs                    # Entry point, DI setup
docs/specs/                     # Requirements (L1, L2)
tests/                          # Quality validation scripts and reports
```

## License

MIT
