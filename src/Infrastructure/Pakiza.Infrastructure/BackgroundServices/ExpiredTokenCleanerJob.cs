
using Pakiza.Application.Services.User;

namespace Pakiza.Infrastructure.BackgroundServices;

public class ExpiredTokenCleanupJob : IJob
{
    private readonly IUsersTokenService _iTokenHandler;
    private readonly ILogger<ExpiredTokenCleanupJob> _logger;

    public ExpiredTokenCleanupJob(IUsersTokenService iTokenHandler, ILogger<ExpiredTokenCleanupJob> logger)
    {
        _iTokenHandler = iTokenHandler;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Quartz: Executing RemoveExpiredTokensAsync");
        await _iTokenHandler.RemoveExpiredTokensAsync();
    }
}
