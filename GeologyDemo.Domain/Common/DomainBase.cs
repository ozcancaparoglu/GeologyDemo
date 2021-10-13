using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeologyDemo.Domain.Common
{
    public abstract class DomainBase
    {
        [Key]
        public int Id { get; set; }

        [Column(Order = 200)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 201)]
        public DateTime? UpdatedDate { get; set; }

        [Column(Order = 202)]
        public int? State { get; set; }

        [Column(Order = 203)]
        public int Order { get; set; }

        [Column(Order = 204)]
        public int ProcessedBy { get; set; }

        public void Activate() => State = (int)Common.State.Active;

        public void Passivated() => State = (int)Common.State.Passive;

        public void Delete() => State = (int)Common.State.Deleted;

    }
}
