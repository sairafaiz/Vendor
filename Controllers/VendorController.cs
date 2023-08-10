using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Text.RegularExpressions;
using VendorMVC.Entities;
using VendorMVC.Models;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Controllers
{
    public class VendorController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;
        public IEnumerable<SelectListItem>? CountryList { get; set; }
        public IEnumerable<SelectListItem>? StateList { get; set; }
        public IEnumerable<SelectListItem>? CityList { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;


        public VendorController(ILogger<HomeController> logger, IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Vendor> objVendorList = _db.Vendor.GetAll().ToList();
            HttpContext.Session.SetString("Id", this.Request.HttpContext.Session.Id);
            return View(objVendorList);
        }

        //Get method to create Vendor
        public IActionResult Upsert(int? id,int? AddressID)
        {
           
            if ((id == null || id == 0) && (AddressID == null || AddressID == 0))
            {
                ViewBag.CountryList = _db.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ViewBag.StateList = _db.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ViewBag.CityList = _db.City.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(new VendorVM());
            }
            else
            {
                var Vendor = _db.Vendor.Get(u => u.Id == id, includeProperties: "City");
                var vendorobj = Vendor.Adapt<VendorVM>();
                if (Vendor==null)
                {
                    var VendorAddress = _db.VendorAddress.Get(u => u.AddressID == AddressID);
                    Vendor = _db.Vendor.Get(u => u.Id == VendorAddress.VendorID, includeProperties: "City");
                     vendorobj = Vendor.Adapt<VendorVM>();
                    vendorobj.VendorAddress = VendorAddress;
                }

                Vendor.City.State = _db.State.Get(u => u.Id == Vendor.City.StateID, includeProperties: "Country");

                int stateId = Vendor.City.StateID;

                int CountryID = Vendor.City.State.CountryID;

                
                ViewBag.CountryList = _db.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ViewBag.StateList = _db.State.GetAll(i => i.Id == stateId).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ViewBag.CityList = _db.City.GetAll(i => i.Id == Vendor.CityID).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

               

                vendorobj.StateID = stateId;
                vendorobj.CountryID = CountryID;

               


                return View(vendorobj);
            }
        }

        public IActionResult stateListView(int? value)
        {
            ViewBag.StateList = _db.State.GetAll(i => i.CountryID == value).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return Json(new { data = ViewBag.StateList });
        }
        public IActionResult CityListView(int? value)
        {

            ViewBag.CityList = _db.City.GetAll(i => i.StateID == value).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return Json(new { data = ViewBag.CityList });
        }

        //Post method to create Vendor
        [HttpPost]
        public IActionResult Upsert(VendorVM vendorobj, IFormFile? file)
        {
            #region Validation
          
            var Session = this.Request.HttpContext.Session.Id; //this.HttpContext.Session.Id;

            List<Vendor> objVendorList = _db.Vendor.GetAll().ToList();

            var DuplyNumber = from s in objVendorList
                           where s.PhoneNumber==vendorobj.PhoneNumber && s.Id != vendorobj.Id
                              select s;
            if(DuplyNumber.Count() >= 1)
                ModelState.AddModelError("PhoneNumber", "Duplicate Phone Number not allowed");

            var DuplyEmail = from s in objVendorList
                             where s.Email==vendorobj.Email && s.Id !=vendorobj.Id
                                  select s;
            if(DuplyEmail.Count() >= 1)
                ModelState.AddModelError("Email", "Duplicate  Email not allowed");

            var DuplyName = from s in objVendorList
                           where s.Name==vendorobj.Name && s.Id != vendorobj.Id
                            select s;
            if(DuplyName.Count() >= 1)
                  ModelState.AddModelError("Name", "Duplicate  Name not allowed");

            #endregion

            if (ModelState.IsValid)
            {

                //FILE upload code
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productpath = Path.Combine(wwwRootPath, @"image\Vendor");

                    using (var fileStream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    vendorobj.imageurl = @"\image\Vendor\" + filename;
               
                }


                var vendor = vendorobj.Adapt<Vendor>();
                vendor.CityID = vendorobj.VendorAddress.CityID;
                // file end
                if (vendorobj.Id == 0)
                {
                    _db.Vendor.Add(vendor);
                }
                else
                {
                    _db.Vendor.Update(vendor);
                }
                _db.save();


                var vendorAddress = _db.VendorAddress.Get(u => u.Session == Session && u.VendorID==null);

                if (vendorobj.VendorAddress.AddressID != 0 || vendorAddress.AddressID!=0)
                {
                    vendorobj.VendorAddress= _db.VendorAddress.Get(u => u.VendorID == vendorobj.Id/*, includeProperties: "City"*/);
                 
                    vendorAddress = vendorobj.VendorAddress;
                    vendorAddress.Session = Session;
                    _db.VendorAddress.Update(vendorAddress);
                }
                else
                {
                    vendorAddress.VendorID = vendor.Id;
                    _db.VendorAddress.Update(vendorAddress);
                }
              
                _db.save();
                TempData["success"] = "Vendor created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CountryList = _db.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ViewBag.StateList = _db.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                ViewBag.CityList = _db.City.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(vendorobj);
            }

         }


        [HttpPost]
        public IActionResult Address(VendorVM vendorVM)
        {
            var Session = this.Request.HttpContext.Session.Id; //this.HttpContext.Session.Id;


            var vendorAddress = vendorVM.VendorAddress.Adapt<VendorAddress>();

            //vendor = _db.VendorAddress.Get(u => u.VendorID == vendorVM.Id);
           

            if (ModelState.IsValid)
            {

                if (vendorAddress.AddressID == 0)
                {
                    vendorAddress.Session = Session;
                    _db.VendorAddress.Add(vendorAddress);
                }
                else
                {
                    vendorAddress = vendorVM.VendorAddress;
                    vendorAddress.VendorID = vendorVM.Id;
                    _db.VendorAddress.Update(vendorAddress);
                }
                _db.save();
                TempData["success"] = "Vendor created successfully";
                return Json(new { data = vendorAddress });
            }
            else
            {
                return View("Upsert", vendorVM);
            }

        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int id)
        {

            List<Vendor> objVendorList = _db.Vendor.GetAll().ToList();
            return Json(new { data = objVendorList});
        }

        [HttpGet]
        public IActionResult GetAllAddress(int VendorAddressID)
        {
            List<VendorAddress> objVendorAddressList = _db.VendorAddress.GetAll(u=>u.VendorID== VendorAddressID).ToList();
            return Json(new { data = objVendorAddressList });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {

            ViewBag.CountryList = _db.Country.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.StateList = _db.State.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CityList = _db.City.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });


            Vendor vendorobj = _db.Vendor.Get(u => u.Id == id, includeProperties: "City");

            vendorobj.City = _db.City.Get(u => u.Id == vendorobj.CityID, includeProperties: "State");

            return View(vendorobj);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Vendortodelete = _db.Vendor.Get(u => u.Id == id);
            if (Vendortodelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Vendor.Remove(Vendortodelete);
            _db.save();

            return Json(new { success = true, message = "Delete Successful" });
        }
           [HttpDelete]
        public IActionResult DeleteAddress(int id)
        {
            var VendorAddresstodelete = _db.VendorAddress.Get(u => u.AddressID == id);
            if (VendorAddresstodelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.VendorAddress.Remove(VendorAddresstodelete);
            _db.save();

            return Json(new { success = true, message = "Delete Successful" });
        }


        #endregion

    }
}
