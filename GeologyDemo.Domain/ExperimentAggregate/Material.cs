using GeologyDemo.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace GeologyDemo.Domain.ExperimentAggregate
{
    public class Material : DomainBase
    {
        [Required]
        [StringLength(250)]
        public string Name { get; private set; }

        public Material(string name)
        {
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
