using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopContext _context;

        public KindergartenServices
            (
                ShopContext context
            )
        {
            _context = context;

        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new Kindergarten();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.Teacher = dto.Teacher;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;


            await _context.Kindergartens.AddAsync(kindergarten);
            await _context.SaveChangesAsync();



            return kindergarten;
        }
        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            var domain = new Kindergarten();
            {
                domain.Id = dto.Id;
                domain.GroupName = dto.GroupName;
                domain.KindergartenName = dto.KindergartenName;
                domain.ChildrenCount = dto.ChildrenCount;
                domain.Teacher = dto.Teacher;

                domain.CreatedAt = dto.CreatedAt;
                domain.UpdatedAt = DateTime.Now;


                _context.Kindergartens.Update(domain);
                await _context.SaveChangesAsync();


                return domain;



            }


        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var kindergartenId = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    SpaceshipId = y.SpaceshipId,
                    ExistingFilePath = y.ExistingFilePath,



                }).ToArrayAsync();

            _context.Kindergartens.Remove(kindergartenId);
            await _context.SaveChangesAsync();

            return kindergartenId;
        }



        public async Task<Kindergarten> GetAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}

