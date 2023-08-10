using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using VendorMVC.Entities;
using VendorMVC.Models;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Controllers
{
    public class StateController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public StateController(ILogger<HomeController> logger, IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
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

            StateVM stateVM = new()
            {
                CountryList = _db.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                State = new State()
            };


            if (id == null || id == 0)
            {
                return View(stateVM);
            }
            else
            {
                stateVM.State = _db.State.Get(u => u.Id == id);
                return View(stateVM);
            }
        }

        //Post method to create 
        [HttpPost]
        public IActionResult Upsert(StateVM stateVM, IFormFile? file)
        {
            List<State> objStateList = _db.State.GetAll(includeProperties: "Country").ToList();

            foreach (var item in objStateList)
            {

                if (item.Id == stateVM.State.Id)
                    continue;

                if (item.Name == stateVM.State.Name)
                    ModelState.AddModelError("Name", "Duplicate  Name not allowed");
               

            }

            if (ModelState.IsValid)
            {

                if (stateVM.State.Id == 0)
                {

                    _db.State.Add(stateVM.State);
                }
                else
                {
                    _db.State.Update(stateVM.State);
                }
                _db.save();
                TempData["success"] = "State created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                stateVM.CountryList = _db.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(stateVM);
            }

        }




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int id)
        {

            List<State> objStateList = _db.State.GetAll(includeProperties: "Country").ToList();

            return Json(new { data = objStateList });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var statetodelete = _db.State.Get(u => u.Id == id);
            if (statetodelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _db.State.Remove(statetodelete);
            _db.save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    
        #endregion






    }
}
