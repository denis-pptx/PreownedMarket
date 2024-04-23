using MediatR;

namespace Chat.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> 
    : IRequest<TResponse>;