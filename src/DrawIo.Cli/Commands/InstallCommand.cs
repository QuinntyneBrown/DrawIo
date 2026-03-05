using System.CommandLine;
using DrawIo.Cli.Services;
using DrawIo.Cli.Skills;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            Console.WriteLine($"Installing {skill} skill...");

            var result = await installer.InstallAsync(skill);

            if (result.Success)
            {
                Console.WriteLine($"[OK] Skill '{skill}' installed successfully.");
                Console.WriteLine($"  Location: {result.InstallPath}");
                Console.WriteLine();
                Console.WriteLine("Claude Code will now detect /drawio as a skill in this project.");
            }
            else
            {
                Console.Error.WriteLine($"[ERROR] Failed to install skill '{skill}'.");
                if (result.ErrorMessage is not null)
                {
                    Console.Error.WriteLine($"  Error: {result.ErrorMessage}");
                }
            }
        }, skillArgument);

        return command;
    }
}
