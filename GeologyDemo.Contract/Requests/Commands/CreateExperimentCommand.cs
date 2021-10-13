using GeologyDemo.Contract.Contracts;
using MediatR;
using System;

namespace GeologyDemo.Contract.Requests.Commands
{
    public class CreateExperimentCommand : IRequest<Result<ExperimentDto>>
    {
        public int? AreaId { get; set; }
        public int? MaterialId { get; set; }
        public string Context { get; set; }
        public string ExcavationMethod { get; set; }
        public int SampleNumber { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
