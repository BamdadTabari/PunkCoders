using System;
using System.Threading.Tasks;
using DataProvider.EntityFramework.Repository;
using Microsoft.Extensions.DependencyInjection;

public class TokenCleanupTask
{
    private readonly IServiceProvider _serviceProvider;

    public TokenCleanupTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ExecuteAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var tokens = await unitOfWork.TokenBlacklistRepo.GetExpiredTokensAsync();
            if (tokens != null)
            {
                unitOfWork.TokenBlacklistRepo.RemoveRange(tokens);
                await unitOfWork.CommitAsync();
            }
        }
    }
}
