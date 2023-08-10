using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using VendorMVC.Entities;
using VendorMVC.Models;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Controllers
{
    public class CityController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CityController(ILogger<HomeController> logger, IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        //Get method to create Vendor
        public IActionResult Upsert(int? id)
        {

            CityVM cityVM = new()
            {
                StateList = _db.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                City = new City()
            };


            if (id == null || id == 0)
            {
                return View(cityVM);
            }
            else
            {
                cityVM.City = _db.City.Get(u => u.Id == id);
                return View(cityVM);
            }
        }

        //Post method to create 
        [HttpPost]
        public IActionResult Upsert(CityVM cityVM, IFormFile? file)
        {
            List<City> objCityList = _db.City.GetAll(includeProperties: "State").ToList();

            foreach (var item in objCityList)
            {

                if (item.Id == cityVM.City.Id)
                    continue;

                if (item.Name == cityVM.City.Name)
                    ModelState.AddModelError("Name", "Duplicate  Name not allowed");
               

            }

            if (ModelState.IsValid)
            {

                if (cityVM.City.Id == 0)
                {

                    _db.City.Add(cityVM.City);
                }
                else
                {
                    _db.City.Update(cityVM.City);
                }
                _db.save();
                TempData["success"] = "State created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                cityVM.StateList = _db.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(cityVM);
            }

        }




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int id)
        {

            List<City> objCityList = _db.City.GetAll(includeProperties: "State").ToList();

            return Json(new { data = objCityList });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Citytodelete = _db.City.Get(u => u.Id == id);
            if (Citytodelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _db.City.Remove(Citytodelete);
            _db.save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    
        #endregion






    }
}
