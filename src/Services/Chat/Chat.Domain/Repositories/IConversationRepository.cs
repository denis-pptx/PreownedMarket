﻿using Chat.Domain.Entities;
using System.Linq.Expressions;

namespace Chat.Domain.Repositories;

public interface IConversationRepository
{
    Task<IEnumerable<Conversation>> GetByUserIdAsync(Guid userId, CancellationToken token = default);
    Task<Conversation?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<Conversation?> FirstOrDefaultAsync(Expression<Func<Conversation, bool>> filter, CancellationToken token = default);
    Task AddAsync(Conversation conversation, CancellationToken token = default);
}