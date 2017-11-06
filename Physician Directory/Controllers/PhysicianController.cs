using Physician_Directory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Physician_Directory.Controllers
{
    public class PhysicianController : Controller
    {
        static Physician physician = new Physician();
        static ContactInfo cont = new ContactInfo();
        static Specialization spec = new Specialization();
        // GET: Physician
        public ActionResult Index(string searchstring)
        {

            TempData["search"] = searchstring;
            TempData.Keep();
            if (TempData["search"] != null)
            {

                var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(searchstring.ToLower()) || s.MiddleName.ToLower().Contains(searchstring.ToLower()) || s.LastName.ToLower().Contains(searchstring.ToLower()) || s.FullName.ToLower().Contains(searchstring.ToLower()));

                return View(searchPhysician);


            }

            return View(physicians);

        }



        public ActionResult ViewContactInfo()

        {


            if (TempData["search"] != null)
            {
                TempData.Keep();
                var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.MiddleName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.LastName.ToLower().Contains(TempData["search"].ToString().ToLower()));


                return View(searchPhysician);


            }



            return View(physicians);

        }

        public ActionResult ViewSpecialization()
        {

            if (TempData["search"] != null)
            {

                var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.MiddleName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.LastName.ToLower().Contains(TempData["search"].ToString().ToLower()));

                TempData.Keep();
                return View(searchPhysician);


            }



            return View(physicians);
        }

        // GET: Physician/Details/5
        public ActionResult Details(int id)
        {
            return View(physicians);
        }

        // GET: Physician/Create
        public ActionResult CreatePhysician()
        {

            return View();
        }

        // POST: Physician/Create
        [HttpPost]
        //[ActionName("Create")]
        //public ActionResult formCollection(FormCollection collection, string nextBtn, string prevBtn)
        public ActionResult CreatePhysician(FormCollection collection, string nextBtn, string prevBtn)
        {
             try
             {

         


            if (nextBtn != null)
            {

                if (ModelState.IsValid)
                {
                    if (physicians.Count > 0)
                    {
                        physician.Id = physicians.Last().Id + 1;
                    }

                    else
                    {
                        physician.Id = 1;
                    }

                    physician.FirstName = collection["FirstName"];
                    physician.MiddleName = collection["MiddleName"];
                    physician.LastName = collection["LastName"];
                    DateTime jDate;
                    //bool isDateFormat = !DateTime.TryParseExact(collection["BirthDate"], "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out jDate);
                    //if (isDateFormat)
                    //{
                    //     ModelState.AddModelError("BirthDate", "Error Format");

                    //   return View(physician);
                    //}
                    DateTime.TryParse(collection["BirthDate"], out jDate);
                    physician.BirthDate = jDate;

                    physician.Gender = collection["Gender"];


                    double weight;
                    double.TryParse(collection["Weight"], out weight);
                    physician.Weight = weight;
                    double height;
                    double.TryParse(collection["Height"], out height);
                    physician.Height = height;



                    return View("AddContactInfo", cont);
                }

            
            }
        
        
            return View();

        } 
         catch
         {
         return View();
         }
    }
    // GET: Physician/Create
    public ActionResult AddContactInfo()
    {

        return View(cont);
    }


    [HttpPost]
    public ActionResult AddContactInfo(FormCollection collection, string prevBtn, string nextBtn)
    {
        try
        {


            if (prevBtn != null)
            {




                return View("CreatePhysician", physician);

            }
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    cont.PhysicianId = physician.Id;
                    cont.HomeAddress = collection["HomeAddress"];
                    cont.HomePhone = (collection["HomePhone"]);
                    cont.OfficeAddress = collection["OfficeAddress"];
                    cont.OfficePhone = (collection["OfficePhone"]);
                    cont.EmailAddress = collection["EmailAddress"];
                    cont.CellphoneNumber = (collection["CellphoneNumber"]);

                    return View("AddSpecialization", spec);
                }
            }
            return View();
        }


        catch
        {
            return View();

        }
    }

    public ActionResult AddSpecialization()
    {

        return View();
    }
    [HttpPost]
    public ActionResult AddSpecialization(FormCollection collection, string prevBtn, string submit)
    {
        try
        {


            if (prevBtn != null)
            {


                return View("AddContactInfo", cont);

            }
            if (submit != null)
            {
                if (ModelState.IsValid)
                {
                    spec.PhysicianId = physician.Id;
                    spec.Name = collection["Name"];
                    spec.Description = collection["Description"];

                    Physician phys = new Physician();

                    phys.Id = physician.Id;
                    phys.FirstName = physician.FirstName;
                    phys.MiddleName = physician.MiddleName;
                    phys.LastName = physician.LastName;
                    phys.BirthDate = physician.BirthDate;
                    phys.Gender = physician.Gender;
                    phys.Height = physician.Height;
                    phys.Weight = physician.Weight;
                    phys.ContactInfo = new ContactInfo
                    {
                        PhysicianId = physician.Id,
                        HomeAddress = cont.HomeAddress,
                        HomePhone = cont.HomePhone,
                        OfficeAddress = cont.OfficeAddress,
                        OfficePhone = cont.OfficePhone,
                        EmailAddress = cont.EmailAddress,
                        CellphoneNumber = cont.CellphoneNumber
                    };
                    phys.Specialization = new Specialization
                    {
                        PhysicianId = physician.Id,
                        Name = spec.Name,
                        Description = spec.Description
                    };
                    physicians.Add(phys);

                    cont = new ContactInfo();
                    spec = new Specialization();
                    return View("Index", physicians);
                }
            }
            return View();
        }


        catch
        {
            return View();
        }
    }

    // GET: Physician/Edit/5
    public ActionResult EditPhysician(int id)
    {

        var phy = physicians.Single(m => m.Id == id);
        return View(phy);

    }

    // POST: Physician/Edit/5
    [HttpPost]
    public ActionResult EditPhysician(int id, FormCollection collection)
    {
        try
        {
            var phy = physicians.Single(m => m.Id == id);
            if (TryUpdateModel(phy))
            {

                return RedirectToAction("Index");
            }
            return View(phy);
        }

        catch
        {
            return View();
        }
    }

    // GET: Physician/Delete/5
    public ActionResult DeletePhysician(int id)
    {
        var selectedPhys = physicians.First(s => s.Id == id);
        return View(selectedPhys);
    }

    // POST: Physician/Delete/5
    [HttpPost]
    public ActionResult DeletePhysician(int id, FormCollection collection)
    {
        try
        {
            var selectedPhys = physicians.First(s => s.Id == id);
            physicians.Remove(selectedPhys);

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }

    }

    public static List<Physician> physicians = new List<Physician>()
        {
            new Physician
            {
                Id = 1, FirstName = "A", MiddleName = "B", LastName="C", BirthDate= DateTime.Now, Gender="Male", Height= 171, Weight= 65,
                ContactInfo = new ContactInfo {PhysicianId = 1, HomeAddress= "Address", HomePhone = "099999", OfficeAddress="Office Address", OfficePhone= "0999", CellphoneNumber="0999", EmailAddress= "michael.dionglay@pointwest.com.ph"  }
                , Specialization = new Specialization { PhysicianId =1, Name="Opthalmologist", Description = "Opthalmologist Description"} },

             new Physician
            {
                Id = 2, FirstName = "B", MiddleName = "C", LastName="D", BirthDate= DateTime.Now, Gender="Male", Height= 171, Weight= 65,
                ContactInfo = new ContactInfo {PhysicianId = 2, HomeAddress= "Address", HomePhone = "099999", OfficeAddress="Office Address", OfficePhone= "0999", CellphoneNumber="0999", EmailAddress= "michael.dionglay@pointwest.com.ph"  }
                , Specialization = new Specialization { PhysicianId =2, Name="Opthalmologist", Description = "Opthalmologist Description"} },

             new Physician
            {
                Id = 3, FirstName = "D", MiddleName = "E", LastName="A", BirthDate= DateTime.Now, Gender="Male", Height= 171, Weight= 65,
                ContactInfo = new ContactInfo {PhysicianId = 3, HomeAddress= "Address", HomePhone = "099999", OfficeAddress="Office Address", OfficePhone= "0999", CellphoneNumber="0999", EmailAddress= "michael.dionglay@pointwest.com.ph"  }
                , Specialization = new Specialization { PhysicianId =3, Name="Opthalmologist", Description = "Opthalmologist Description"} }

        };

    //static List<Physician> physicians = new List<Physician>();






}


}
