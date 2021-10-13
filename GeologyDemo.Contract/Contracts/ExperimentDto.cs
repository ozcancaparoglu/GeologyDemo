using GeologyDemo.Contract.Contracts.Common;
using System;

namespace GeologyDemo.Contract.Contracts
{
    public class ExperimentDto : DtoBase
    {
        public int? AreaId { get; set; }
        public AreaDto Area { get; set; }
        public int? MaterialId { get; set; }
        public MaterialDto Material { get; set; }
        public string Context { get; set; }
        public string ExcavationMethod { get; set; }
        public int SampleNumber { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
