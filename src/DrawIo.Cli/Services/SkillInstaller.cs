using System.Text.Json;
using DrawIo.Cli.Skills;
using Microsoft.Extensions.Logging;

namespace DrawIo.Cli.Services;

public class SkillInstaller(ILogger<SkillInstaller> logger) : ISkillInstaller
{
    private static readonly string SkillsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".drawio",
        "skills");

    public async Task<InstallResult> InstallAsync(string skillName, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Installing skill: {SkillName}", skillName);

        if (!string.Equals(skillName, DrawIoSkill.Name, StringComparison.OrdinalIgnoreCase))
        {
            var message = $"Unknown skill '{skillName}'. Available skills: {DrawIoSkill.Name}";
            logger.LogError(message);
            return new InstallResult(false, string.Empty, message);
        }

        try
        {
            Directory.CreateDirectory(SkillsDirectory);

            var skillDefinition = DrawIoSkill.GetDefinition();
            var filePath = Path.Combine(SkillsDirectory, $"{skillDefinition.Name}.json");

            var json = JsonSerializer.Serialize(skillDefinition, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(filePath, json, cancellationToken);

            logger.LogInformation("Skill '{SkillName}' installed successfully to {FilePath}", skillName, filePath);
            return new InstallResult(true, filePath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to install skill '{SkillName}'", skillName);
            return new InstallResult(false, string.Empty, ex.Message);
        }
    }
}
