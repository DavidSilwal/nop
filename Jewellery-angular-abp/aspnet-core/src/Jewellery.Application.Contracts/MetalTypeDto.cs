using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Jewellery
{
    public class MetalTypeDto : AuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }


        [Required]
        public decimal Cost { get; set; }
    }

    public class CreateUpdateMetalTypeDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }


        [Required]
        public decimal Cost { get; set; }
    }
}
