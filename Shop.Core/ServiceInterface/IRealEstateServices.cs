using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> Create(RealEstateDto dto);

        Task<RealEstate> GetAsync(Guid id);

        Task<RealEstate> Update(RealEstateDto dto);

        Task<RealEstate> Delete(Guid id);
    }
}
