﻿using DataProvider.EntityFramework.Repository;

namespace PunkCoders.Configs;
public class TokenCleanupTask
{
    public async Task ExecuteAsync(IUnitOfWork unitOfWork)
    {
        var tokens = await unitOfWork.TokenBlacklistRepo.GetExpiredTokensAsync();
        if (tokens != null)
        {
            unitOfWork.TokenBlacklistRepo.RemoveRange(tokens);
            await unitOfWork.CommitAsync();
        }
    }
}
