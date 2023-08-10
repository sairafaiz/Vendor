using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using VendorMVC.Entities;
using VendorMVC.Repository.IRepository;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VendorMVC.Controllers
{
    public class CountryController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CountryController(ILogger<HomeController> logger, IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
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

            if (id == null || id == 0)
            {
                return View(new Country());
            }
            else
            {
                Country Countryobj = _db.Country.Get(u => u.Id == id);
                return View(Countryobj);
            }
        }

        //Post method to create 
        [HttpPost]
        public IActionResult Upsert(Country Countryobj, IFormFile? file)
        {


            Match match = Regex.Match(Countryobj.Code.ToString(), "^[0-9]{4}$") ;


            if (!match.Success)
                ModelState.AddModelError("Code", "invalid Code");


            List<Country> objCountryList = _db.Country.GetAll().ToList();

            foreach (var item in objCountryList)
            {
                if (item.Id == Countryobj.Id)
                    continue;

                if (item.Name == Countryobj.Name)
                    ModelState.AddModelError("Name", "Duplicate  Name not allowed");

                if (item.Code == Countryobj.Code)
                    ModelState.AddModelError("Code", "Duplicate  Code not allowed");
            }

            if (ModelState.IsValid)
            {
                //FILE upload code
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productpath = Path.Combine(wwwRootPath, @"image\Country");

                    using (var fileStream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    Countryobj.flag = @"\image\Country\" + filename;
                }
                
                // file end
                if (Countryobj.Id == 0)
                {

                    _db.Country.Add(Countryobj);
                }
                else
                {
                    _db.Country.Update(Countryobj);
                }
                _db.save();
                TempData["success"] = "Vendor created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(Countryobj);
            }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int? id)
        {

            List<Country> objCountryList = _db.Country.GetAll().ToList();

            return Json(new { data = objCountryList });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Countrytodelete = _db.Country.Get(u => u.Id == id);
            if (Countrytodelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _db.Country.Remove(Countrytodelete);
            _db.save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    
        #endregion



    }
}
