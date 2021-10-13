using GeologyDemo.Application.Services;
using GeologyDemo.Contract.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeologyDemo.Web.Pages
{
    public class DatatableModel : PageModel
    {
        private readonly IFillingService _fillingService;

        public List<ExperimentDto> Experiments { get; set; }

        public DatatableModel(IFillingService fillingService)
        {
            _fillingService = fillingService;
        }

        public async Task OnGet()
        {
            Experiments = await _fillingService.GetExperiments();
        }
    }
}
