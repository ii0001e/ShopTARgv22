using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IKindergartenServices
    {
        Task<Kindergarten> Create(KindergartenDto dto);

        Task<Kindergarten> GetAsync(Guid id);

        Task<Kindergarten> Update(KindergartenDto dto);

        Task<Kindergarten> Delete(Guid id);
    }
}
