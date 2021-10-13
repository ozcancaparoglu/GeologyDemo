using GeologyDemo.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeologyDemo.Domain.ExperimentAggregate
{
    public class Experiment : DomainBase
    {
        public int? AreaId { get; private set; }
        [ForeignKey("AreaId")]
        public Area Area { get; private set; }
        public int? MaterialId { get; private set; }
        [ForeignKey("MaterialId")]
        public Material Material { get; private set; }
        [Required]
        [StringLength(250)]
        public string Context { get; private set; }
        [Required]
        [StringLength(250)]
        public string ExcavationMethod { get; private set; }
        public int SampleNumber { get; private set; }
        [Required]
        [StringLength(2500)]
        public string Description { get; private set; }
        public DateTime OpenDate { get; private set; }
        public DateTime? CloseDate { get; private set; }

        public Experiment(string context, string excavationMethod, int sampleNumber, string description, DateTime openDate, DateTime? closeDate)
        {
            Context = context;
            ExcavationMethod = excavationMethod;
            SampleNumber = sampleNumber;
            Description = description;
            OpenDate = openDate;
            CloseDate = closeDate;
        }

        public Experiment(string context, string excavationMethod, int sampleNumber, string description, DateTime openDate, DateTime? closeDate, Area area, Material material)
            : this(context, excavationMethod, sampleNumber, description, openDate, closeDate)
        {
            Context = context;
            ExcavationMethod = excavationMethod;
            SampleNumber = sampleNumber;
            Description = description;
            OpenDate = openDate;
            CloseDate = closeDate;
            Area = area;
            Material = material;
        }

        public void SetExperiment(Area area, Material material, string context, string excavationMethod, int sampleNumber, string description, DateTime openDate, DateTime? closeDate)
        {
            Area = area;
            Material = material;
            Context = context;
            ExcavationMethod = excavationMethod;
            SampleNumber = sampleNumber;
            Description = description;
            OpenDate = openDate;
            CloseDate = closeDate;
        }
    }
}
