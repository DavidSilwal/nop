using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jewellery
{
    public interface IMetalTypeService:
        ICrudAppService< //Defines CRUD methods
            MetalTypeDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
            CreateUpdateMetalTypeDto, //Used to create a new book
            CreateUpdateMetalTypeDto> //Used to update a book
    {

    }
  
}
