using System.CommandLine;
using DrawIo.Cli.Commands;
using DrawIo.Cli.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddFilter("Microsoft", LogLevel.Warning);
        logging.AddFilter("System", LogLevel.Warning);
    })
    .ConfigureServices(services =>
    {
        services.AddTransient<ISkillInstaller, SkillInstaller>();
    })
    .Build();

var services = host.Services;

AnsiConsole.Write(
    new FigletText("DrawIo CLI")
        .Centered()
        .Color(Color.Blue));

AnsiConsole.MarkupLine("[dim]Draw.io skill management tool[/]");
AnsiConsole.WriteLine();

var rootCommand = new RootCommand("DrawIo CLI – manage Draw.io skills for AI agents.");

rootCommand.AddCommand(InstallCommand.Create(services));

return await rootCommand.InvokeAsync(args);
