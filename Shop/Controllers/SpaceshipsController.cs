using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Spaceship;
using static System.Net.Mime.MediaTypeNames;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopContext _context;
        private readonly ISpaceshipServices _spaceshipServices;
        private readonly IFileServices _fileServices;
        

        public SpaceshipsController
            (
            ShopContext context,
            ISpaceshipServices spaceshipServices,
            IFileServices fileServices
            
            ) 
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
            _fileServices = fileServices;
            
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaeshipsIndexViewModel
                {
                    Id= x.Id,
                    Name= x.Name, 
                    Type= x.Type, 
                    EnginPower= x.EnginPower,
                    Passangers= x.Passangers,
                });

            return View(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateUpdateViewModel spaceship =new SpaceshipCreateUpdateViewModel();

            return View("CreateUpdate", spaceship);
        }         


        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passangers = vm.Passangers,
                EnginPower = vm.EnginPower,
                Crew = vm.Crew,
                Company = vm.Company,
                CargoWeight = vm.CargoWeight,
                Files=vm.Files,
                Image=vm.FileToApiViewModels
                    .Select(x=>new FileToApiDto
                    {
                        Id= x.ImageId,
                        ExistingFilePath=x.FilePath,
                        SpaceshipId=x.SpaceshipId,
                    }).ToArray()

            };

            var result = await _spaceshipServices.Create(dto);

            if (result==null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index), vm);
            //return index
            
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);

            if (spaceship==null)
            {
                return NotFound();

            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new SpaceshipDetailsViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            
            vm.Passangers= spaceship.Passangers;
            
            vm.EnginPower= spaceship.EnginPower;
            vm.Crew = spaceship.Crew;
            vm.Company= spaceship.Company;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.CreatedAt= spaceship.CreatedAt;
            vm.Modifieted= spaceship.Modifieted;
            vm.FileToApiViewModels.AddRange(images);
            


            return View(vm);



        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id); 

            if(spaceship==null)
            {
                return NotFound();
            }
            var images = await _context.FileToApis
              .Where(x => x.SpaceshipId == id)
              .Select(y => new FileToApiViewModel
              {
                  FilePath = y.ExistingFilePath,
                  ImageId = y.Id
              }).ToArrayAsync();


            var vm = new SpaceshipCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;

            vm.Passangers = spaceship.Passangers;

            vm.EnginPower = spaceship.EnginPower;
            vm.Crew = spaceship.Crew;
            vm.Company = spaceship.Company;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.Modifieted = spaceship.Modifieted;
            vm.FileToApiViewModels.AddRange(images);



            return View("CreateUpdate",vm);
        }
        [HttpPost]    
        public async  Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passangers = vm.Passangers,
                EnginPower = vm.EnginPower,
                Crew = vm.Crew,
                Company = vm.Company,
                CargoWeight = vm.CargoWeight,
                CreatedAt = vm.CreatedAt,
                Modifieted = DateTime.Now,
                Files=vm.Files,
                Image = vm.FileToApiViewModels 
                .Select(x =>new FileToApiDto
                {
                    Id= x.ImageId,
                    ExistingFilePath = x.FilePath,
                    SpaceshipId= x.SpaceshipId
                }).ToArray()
            };
            var result=await _spaceshipServices.Update(dto);
            if (result==null)
            {
                return RedirectToAction(nameof(Index),vm);

            }
            return RedirectToAction(nameof(Index), vm);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship=await _spaceshipServices.GetAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }
            var images = await _context.FileToApis
             .Where(x => x.SpaceshipId == id)
             .Select(y => new FileToApiViewModel
             {
                 FilePath = y.ExistingFilePath,
                 ImageId = y.Id
             }).ToArrayAsync();
            var vm = new SpaceshipDeleteViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;

            vm.Passangers = spaceship.Passangers;

            vm.EnginPower = spaceship.EnginPower;
            vm.Crew = spaceship.Crew;
            vm.Company = spaceship.Company;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.Modifieted = spaceship.Modifieted;
            vm.FileToApiViewModels.AddRange(images);

            return View( vm);
        }
       
        [HttpPost] 
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceshipId = await _spaceshipServices.Delete(id);

            if (spaceshipId==null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewModel vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId,
            };

            var image = await _fileServices.RemoveImageFromApi(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }





    }

}
