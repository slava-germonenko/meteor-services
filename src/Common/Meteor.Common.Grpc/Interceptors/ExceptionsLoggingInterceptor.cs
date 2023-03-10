using System.Text.Json;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Meteor.Common.Grpc.Interceptors;

public class ExceptionsLoggingInterceptor : Interceptor
{
    private readonly ILogger<ExceptionsLoggingInterceptor> _logger;

    public ExceptionsLoggingInterceptor(ILogger<ExceptionsLoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation
    )
    {
        try
        {
            return continuation(request, context);
        }
        catch (Exception e)
        {
            LogRequestError(request, e);
            throw;
        }
    }

    public override TResponse BlockingUnaryCall<TRequest, TResponse>(
        TRequest request, 
        ClientInterceptorContext<TRequest, TResponse> context,
        BlockingUnaryCallContinuation<TRequest, TResponse> continuation
    )
    {
        try
        {
            return continuation(request, context);
        }
        catch (Exception e)
        {
            LogRequestError(request, e);
            throw;
        }
    }

    private void LogRequestError<TRequest>(TRequest request, Exception e)
    {
        _logger.LogError(e, "Failed to handle request: {0}", JsonSerializer.Serialize(request));
    }
}