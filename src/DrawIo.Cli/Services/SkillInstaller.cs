using DrawIo.Cli.Skills;
using Microsoft.Extensions.Logging;

namespace DrawIo.Cli.Services;

public class SkillInstaller(ILogger<SkillInstaller> logger) : ISkillInstaller
{
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
            var skillDir = Path.Combine(Directory.GetCurrentDirectory(), ".claude", "skills", DrawIoSkill.Name);
            Directory.CreateDirectory(skillDir);

            var filePath = Path.Combine(skillDir, "SKILL.md");
            var content = DrawIoSkill.GetSkillMarkdown();

            await File.WriteAllTextAsync(filePath, content, cancellationToken);

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
