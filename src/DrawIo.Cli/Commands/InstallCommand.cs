using System.CommandLine;
using DrawIo.Cli.Services;
using DrawIo.Cli.Skills;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace DrawIo.Cli.Commands;

public static class InstallCommand
{
    public static Command Create(IServiceProvider services)
    {
        var skillArgument = new Argument<string>(
            name: "skill",
            description: "The name of the skill to install.",
            getDefaultValue: () => DrawIoSkill.Name);

        var command = new Command("install", "Install a Draw.io skill for an agent.")
        {
            skillArgument
        };

        command.SetHandler(async (string skill) =>
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            var installer = services.GetRequiredService<ISkillInstaller>();

            logger.LogInformation("Starting install for skill: {Skill}", skill);

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("blue bold"))
                .StartAsync($"Installing [bold blue]{skill}[/] skill...", async _ =>
                {
                    var result = await installer.InstallAsync(skill);

                    if (result.Success)
                    {
                        AnsiConsole.MarkupLine($"[bold green]✓[/] Skill [bold]{skill}[/] installed successfully.");
                        AnsiConsole.MarkupLine($"  Location: [dim]{result.InstallPath}[/]");
                        AnsiConsole.WriteLine();

                        var panel = new Panel(
                            new Markup(
                                "[yellow]The Draw.io skill has been installed.[/]\n\n" +
                                "The skill instructs an agent to create professional Draw.io diagrams,\n" +
                                "including flowcharts, sequence diagrams, ER diagrams, UML class diagrams,\n" +
                                "network diagrams, mind maps, swimlane diagrams, and architecture diagrams."))
                        {
                            Header = new PanelHeader("[bold]Draw.io Skill[/]"),
                            Border = BoxBorder.Rounded,
                            Padding = new Padding(1, 0)
                        };

                        AnsiConsole.Write(panel);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[bold red]✗[/] Failed to install skill [bold]{skill}[/].");
                        if (result.ErrorMessage is not null)
                        {
                            AnsiConsole.MarkupLine($"  Error: [red]{Markup.Escape(result.ErrorMessage)}[/]");
                        }
                    }
                });
        }, skillArgument);

        return command;
    }
}
