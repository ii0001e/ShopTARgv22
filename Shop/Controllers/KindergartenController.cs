using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Kindergarten;

namespace Shop.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopContext _context;
        private readonly IKindergartenServices _kindergartenServices;


        public KindergartenController
            (
            ShopContext context,
            IKindergartenServices spaceshipServices

            )
        {
            _context = context;
            _kindergartenServices = spaceshipServices;

        }

        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    KindergartenName = x.KindergartenName,
                    ChildrenCount = x.ChildrenCount,
                    Teacher = x.Teacher
                });

            return View(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel kindergarten = new KindergartenCreateUpdateViewModel();

            return View("CreateUpdate", kindergarten);
        }


        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel kg)
        {
            var dto = new KindergartenDto()
            {
                Id = kg.Id,
                GroupName= kg.GroupName,
                KindergartenName= kg.KindergartenName,
                ChildrenCount= kg.ChildrenCount,
                Teacher= kg.Teacher
               
            };

            var result = await _kindergartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index), kg);
            //return index

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarten = await _kindergartenServices.GetAsync(id);

            if (kindergarten == null)
            {
                return NotFound();

            }

            var kg = new KindergartenDetailsViewModel();

            kg.Id = kindergarten.Id;
            kg.GroupName = kindergarten.GroupName;
            kg.KindergartenName = kindergarten.KindergartenName;
            kg.ChildrenCount = kindergarten.ChildrenCount;
            kg.Teacher = kindergarten.Teacher;

            kg.CreatedAt = kindergarten.CreatedAt;
            kg.UpdatedAt = kindergarten.UpdatedAt;



            return View(kg);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.GetAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }
            
            var kg = new KindergartenCreateUpdateViewModel();

            kg.Id = kindergarten.Id;
            kg.GroupName = kindergarten.GroupName;
            kg.KindergartenName = kindergarten.KindergartenName;
            kg.ChildrenCount = kindergarten.ChildrenCount;
            kg.Teacher = kindergarten.Teacher;

            kg.CreatedAt = kindergarten.CreatedAt;
            kg.UpdatedAt = kindergarten.UpdatedAt;

            return View("CreateUpdate", kg);
        }
        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel kg)
        {
            var dto = new KindergartenDto()
            {
                Id = kg.Id,
                GroupName = kg.GroupName,
                KindergartenName = kg.KindergartenName,
                ChildrenCount = kg.ChildrenCount,
                Teacher = kg.Teacher,
                
                CreatedAt = kg.CreatedAt,
                UpdatedAt = DateTime.Now,
                
            };
            var result = await _kindergartenServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index), kg);

            }
            return RedirectToAction(nameof(Index), kg);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarten = await _kindergartenServices.GetAsync(id);
            if (kindergarten == null)
            {
                return NotFound();
            }
           var kg = new KindergartenDeleteViewModel();

            kg.Id = kindergarten.Id;
            kg.GroupName = kindergarten.GroupName;
            kg.KindergartenName = kindergarten.KindergartenName;
            kg.ChildrenCount = kindergarten.ChildrenCount;
            kg.Teacher = kindergarten.Teacher;

            kg.CreatedAt = kindergarten.CreatedAt;
            kg.UpdatedAt = kindergarten.UpdatedAt;

            return View(kg);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kindergartenId = await _kindergartenServices.Delete(id);

            if (kindergartenId == null)
            {
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
