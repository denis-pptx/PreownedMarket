﻿using MediatR;

namespace Identity.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;