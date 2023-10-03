using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.ApplicationServices.Services;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Realestate;
using Shop.Models.Spaceship;

namespace Shop.Controllers
{
    
    public class RealestateController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstateServices _realstateService; // Inject the RealEstate service
         // Add the RealEstate service to constructor


        public RealestateController
           (
           ShopContext context,

           IRealEstateServices realstateService
           )
        {
            _context = context;
            
            _realstateService = realstateService;
        }


        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    SizeSqrM = x.SizeSqrM,
                    RoomCount = x.RoomCount,
                    Floor = x.Floor,
                });

            return View(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel realstate = new RealEstateCreateUpdateViewModel();

            return View("CreateUpdate", realstate);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                SizeSqrM =Convert.ToSingle( vm.SizeSqrM),
                RoomCount = vm.RoomCount,
                Floor = vm.Floor,

                BuildingType = vm.BuildingType,
                BuiltinYear = vm.BuiltinYear,
                
                Files = vm.Files,
                Image = vm.FileToApiViewModels
                    .Select(x => new FileToApiDto
                    {
                        Id = x.Id,
                        ExistingFilePath = x.FilePath,
                        //1.filetoapidto 2.filetoapiviewmodel
                        RealsetateId = x.RealsetateId,
                    }).ToArray()

            };

            var result = await _realstateService.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index), vm);
            //return index

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realestate = await _realstateService.GetAsync(id);

            if (realestate == null)
            {
                return NotFound();

            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new FileToApiRealViewModel
                {
                    FilePath = y.ExistingFilePath,
                    Id = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.SizeSqrM = (float)realestate.SizeSqrM;

            vm.RoomCount = realestate.RoomCount;

            vm.Floor = realestate.Floor;
            vm.BuildingType = realestate.BuildingType;
            vm.BuiltinYear = realestate.BuiltinYear;
            //string formattedDate = realestate.BuiltinYear.ToString("yyyy-MM-dd HH:mm:ss");

            vm.CreatedAt = realestate.CreatedAt;
            vm.UpdatedAt = realestate.UpdatedAt;
            vm.FileToApiViewModels.AddRange((IEnumerable<Models.Realestate.FileToApiRealViewModel>)images);



            return View(vm);



        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realstate = await _realstateService.GetAsync(id);

            if (realstate == null)
            {
                return NotFound();
            }


            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realstate.Id;
            vm.Address = realstate.Address;
            vm.SizeSqrM = (float)realstate.SizeSqrM;

            vm.RoomCount = realstate.RoomCount;

            vm.Floor = realstate.Floor;
            vm.BuildingType = realstate.BuildingType;
            vm.BuiltinYear = realstate.BuiltinYear;
            
            vm.CreatedAt = realstate.CreatedAt;
            vm.UpdatedAt = realstate.UpdatedAt;

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                SizeSqrM = Convert.ToSingle( vm.SizeSqrM),
                RoomCount = vm.RoomCount,
                Floor = vm.Floor,

                BuildingType = vm.BuildingType,
                
                
                CreatedAt = vm.CreatedAt,
                UpdatedAt = DateTime.Now,
            };
            var result = await _realstateService.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index), vm);

            }
            return RedirectToAction(nameof(Index), vm);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realstate = await _realstateService.GetAsync(id);
            if (realstate == null)
            {
                return NotFound();
            }
            var vm = new RealEstateDeleteViewModel();

            vm.Id = realstate.Id;
            vm.Address = realstate.Address;
            vm.SizeSqrM = (float)realstate.SizeSqrM;

            vm.RoomCount = realstate.RoomCount;

            vm.Floor = realstate.Floor;
            vm.BuildingType = realstate.BuildingType;
            vm.BuiltinYear = realstate.BuiltinYear;
            
            vm.CreatedAt = realstate.CreatedAt;
            vm.UpdatedAt = realstate.UpdatedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realestateId = await _realstateService.Delete(id);

            if (realestateId == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }







    }
}

