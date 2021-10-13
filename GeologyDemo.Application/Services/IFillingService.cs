using GeologyDemo.Contract.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeologyDemo.Application.Services
{
    public interface IFillingService
    {
        Task<List<AreaDto>> GetAreas();
        Task<List<ExperimentDto>> GetExperiments();
        Task<List<MaterialDto>> GetMaterials();
    }
}