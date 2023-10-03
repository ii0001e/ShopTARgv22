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
    public class RealEstatesServices : IRealEstateServices
    {

        private readonly ShopContext _context;
        private readonly IFileServices _fileServices;

        public RealEstatesServices
            (
                ShopContext context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate realestate = new RealEstate();

            realestate.Id = Guid.NewGuid();
            realestate.Address = dto.Address;
            realestate.SizeSqrM = dto.SizeSqrM;
            realestate.RoomCount = dto.RoomCount;
            realestate.Floor = dto.Floor;
            realestate.BuildingType = dto.BuildingType;
            
            realestate.BuiltinYear = dto.BuiltinYear;
            realestate.CreatedAt = DateTime.Now;
            realestate.UpdatedAt = DateTime.Now;
            //_fileServices.FilesToApi(dto, realestate);


            if (dto.Files !=null)
            {
                _fileServices.UploadFilesToDatabase(dto, realestate)

            }

            await _context.RealEstates.AddAsync(realestate);
            await _context.SaveChangesAsync();



            return realestate;
        }
        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var domain = new RealEstate()
            {
                Id = dto.Id,
                Address = dto.Address,
                SizeSqrM = dto.SizeSqrM,
                RoomCount = dto.RoomCount,
                Floor = dto.Floor,
                BuildingType = dto.BuildingType,
                BuiltinYear = dto.BuiltinYear,
                
                CreatedAt = dto.CreatedAt,
                UpdatedAt = DateTime.Now,



            };

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();


            return domain;
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var realestateId = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            //_context.RealEstates.Remove(realestateId);
            await _context.SaveChangesAsync();

            return realestateId;
        }



        public async Task<RealEstate> GetAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }


}
