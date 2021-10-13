using GeologyDemo.Application.Services;
using GeologyDemo.Contract.Contracts;
using GeologyDemo.Contract.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeologyDemo.Web.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;
        private readonly IFillingService _fillingService;
        public string Area { get; set; }
        public string Context { get; set; }
        public string ExcavationMethod { get; set; }
        public int SampleNumber { get; set; }
        public string Material { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Description { get; set; }

        public List<AreaDto> Areas { get; set; }
        public List<MaterialDto> Materials { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator, IFillingService fillingService)
        {
            _logger = logger;
            _mediator = mediator;
            _fillingService = fillingService;
        }

        public async Task OnGet()
        {
            Areas = await _fillingService.GetAreas();
            Materials = await _fillingService.GetMaterials();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var experiment = new CreateExperimentCommand()
                {
                    AreaId = int.Parse(Area),
                    MaterialId = int.Parse(Material),
                    Context = Context,
                    ExcavationMethod = ExcavationMethod,
                    Description = Description,
                    CloseDate = CloseDate,
                    OpenDate = OpenDate,
                    SampleNumber = SampleNumber
                };

                await _mediator.Send(experiment);

                return RedirectToPage("Datatable");
            }
            catch
            {
                return new PageResult();
            }
        }
    }
}
