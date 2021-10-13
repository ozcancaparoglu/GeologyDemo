using GeologyDemo.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeologyDemo.Application.Handler
{
    public class HealthCheckQueryHandler : IRequestHandler<HealthCheckQuery, Result<object>>
    {
        public Task<Result<object>> Handle(HealthCheckQuery request, CancellationToken cancellationToken)
        {
            return Result<object>.SuccessAsync("Asayiş Berkemal Hacıt");
        }
    }
}
