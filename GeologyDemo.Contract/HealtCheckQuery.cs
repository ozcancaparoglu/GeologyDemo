using MediatR;

namespace GeologyDemo.Contract
{
    public class HealthCheckQuery : IRequest<Result<object>>
    {
    }
}
