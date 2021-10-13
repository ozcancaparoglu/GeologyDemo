using GeologyDemo.Contract;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GeologyDemo.Container.Decorator
{
    public class ExceptionHandler<TRequest, TResponse> : DecoratorBase<TRequest, TResponse>
        where TResponse : Result<TResponse>, new() where TRequest : IRequest<TResponse>
    {
        public override async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
         RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            var handlerMethodInfo = GetHandlerMethodInfo();
            try
            {
                response = await next();
            }
            catch (Exception exception)
            {
                //TODO: Log with handlerMethodInfo
                switch (exception)
                {
                    //http timeout
                    case TaskCanceledException:
                    case AggregateException:
                        response = new TResponse
                        {
                            Succeeded = false,
                            Errors = new string[] { exception.Message }
                        };
                        break;
                    //unexpected http response received
                    case HttpRequestException:
                    case JsonReaderException:
                        response = new TResponse
                        {
                            Succeeded = false,
                            Errors = new string[] { exception.Message }
                        };
                        break;
                    default:
                        response = new TResponse
                        {
                            Succeeded = false,
                            Errors = new string[] { exception.Message }
                        };
                        break;
                }

            }

            return response;
        }
    }
}
