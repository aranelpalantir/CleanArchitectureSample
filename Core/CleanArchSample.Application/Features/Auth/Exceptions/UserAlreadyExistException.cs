﻿using System.Net;
using CleanArchSample.Application.Exceptions;
using CleanArchSample.SharedKernel;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class UserAlreadyExistException() : BaseBusinessRuleException("Böyle bir kullanıcı zaten var!", ErrorType.Conflict);
}
