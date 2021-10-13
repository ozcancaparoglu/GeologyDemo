using GeologyDemo.Application.AutoMapper;
using GeologyDemo.Contract.Contracts;
using GeologyDemo.Domain;
using GeologyDemo.Domain.ExperimentAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeologyDemo.Application.Services
{
    public class FillingService : IFillingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAutoMapperConfiguration _autoMapper;

        public FillingService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }


        public async Task<List<AreaDto>> GetAreas()
        {
            var entities = await _unitOfWork.Repository<Area>().GetAll();
            return _autoMapper.MapCollection<Area, AreaDto>(entities);
        }

        public async Task<List<MaterialDto>> GetMaterials()
        {
            var entities = await _unitOfWork.Repository<Material>().GetAll();
            return _autoMapper.MapCollection<Material, MaterialDto>(entities);
        }

        public async Task<List<ExperimentDto>> GetExperiments()
        {
            var entities = await _unitOfWork.Repository<Experiment>().FilterWithProperties(null,"Area,Material");
            return _autoMapper.MapCollection<Experiment, ExperimentDto>(entities);
        }


    }
}
