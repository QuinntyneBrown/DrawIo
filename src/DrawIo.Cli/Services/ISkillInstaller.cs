namespace DrawIo.Cli.Services;

public interface ISkillInstaller
{
    Task<InstallResult> InstallAsync(string skillName, CancellationToken cancellationToken = default);
}

public record InstallResult(bool Success, string InstallPath, string? ErrorMessage = null);
