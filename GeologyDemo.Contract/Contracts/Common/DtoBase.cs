using System;

namespace GeologyDemo.Contract.Contracts.Common
{
    public class DtoBase
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? State { get; set; }
        public int Order { get; set; }
        public int ProcessedBy { get; set; }
    }
}
