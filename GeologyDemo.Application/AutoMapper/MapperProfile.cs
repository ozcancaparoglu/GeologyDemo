using AutoMapper;
using GeologyDemo.Contract.Contracts;
using GeologyDemo.Contract.Requests.Commands;
using GeologyDemo.Domain.ExperimentAggregate;

namespace GeologyDemo.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        private readonly int depth = 5;

        public MapperProfile()
        {
            CreateMap<CreateExperimentCommand, Experiment>().MaxDepth(depth);
            CreateMap<Experiment, ExperimentDto>().MaxDepth(depth);
            CreateMap<Area, AreaDto>().MaxDepth(depth);
            CreateMap<Material, MaterialDto>().MaxDepth(depth);
        }

    }
}
