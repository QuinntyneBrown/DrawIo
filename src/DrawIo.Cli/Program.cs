using System.CommandLine;
using DrawIo.Cli.Commands;
using DrawIo.Cli.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

Console.WriteLine("DrawIo CLI");
Console.WriteLine("Draw.io skill management tool");
Console.WriteLine();

var rootCommand = new RootCommand("DrawIo CLI – manage Draw.io skills for AI agents.");

rootCommand.AddCommand(InstallCommand.Create(services));

return await rootCommand.InvokeAsync(args);
