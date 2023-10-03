using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);
                          //array a few file at the same time
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);

        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);


        void UploadFilesToDatabase(SpaceshipDto dto, Spaceship domain);


        //void FilesToApi(RealEstateDto dto, RealEstate realestate);

    }
  
        
    
}
