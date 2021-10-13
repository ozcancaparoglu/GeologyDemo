using GeologyDemo.Application.AutoMapper;
using GeologyDemo.Contract;
using GeologyDemo.Contract.Contracts;
using GeologyDemo.Contract.Requests.Commands;
using GeologyDemo.Domain;
using GeologyDemo.Domain.ExperimentAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeologyDemo.Application.Handler.Commands
{
    public class CreateExperimentCommandHandler : IRequestHandler<CreateExperimentCommand, Result<ExperimentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAutoMapperConfiguration _autoMapper;

        public CreateExperimentCommandHandler(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<Result<ExperimentDto>> Handle(CreateExperimentCommand request, CancellationToken cancellationToken)
        {
            var entity = _autoMapper.MapObject<CreateExperimentCommand, Experiment>(request);

            await _unitOfWork.Repository<Experiment>().Add(entity);

            await _unitOfWork.CommitAsync();

            return await Result<ExperimentDto>.SuccessAsync(_autoMapper.MapObject<Experiment, ExperimentDto>(entity));
        }
    }
}
