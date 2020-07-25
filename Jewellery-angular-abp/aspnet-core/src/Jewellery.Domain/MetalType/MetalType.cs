using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jewellery.MetalType
{
    public class MetalType : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }
}
