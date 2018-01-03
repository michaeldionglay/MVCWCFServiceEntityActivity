using Physician_Directory.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
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

        ServiceReference1.MyServiceClient ur = new ServiceReference1.MyServiceClient();

        public ActionResult Index(string searchstring)
        {
            TempData["search"] = searchstring;
            TempData.Keep();
            
            if (searchstring != null)
            {
                List<Physician> physi = new List<Physician>();


                var searchPhysician = ur.GetPhysicianByName(searchstring);

                foreach (var item in searchPhysician)
                {
                    Physician phys = new Physician();
                    phys.Id = item.Id;
                    phys.FirstName = item.FirstName;
                    phys.MiddleName = item.MiddleName;
                    phys.LastName = item.LastName;
                    phys.BirthDate = item.BirthDate;
                    phys.Gender = item.Gender;
                    phys.Height = item.Height;
                    phys.Weight = item.Weight;
                    phys.ContactInfo = new ContactInfo
                    {
                        PhysicianId = item.Id,
                        HomeAddress = item.ContactInformation.HomeAddress,
                        HomePhone = item.ContactInformation.HomePhone,
                        OfficeAddress = item.ContactInformation.OfficeAddress,
                        OfficePhone = item.ContactInformation.OfficePhone,
                        EmailAddress = item.ContactInformation.EmailAddress,
                        CellphoneNumber = item.ContactInformation.CellphoneNumber
                    };
                    phys.Specialization = new Specialization
                    {
                        PhysicianId = item.Id,
                        Name = item.Specialization.Name,
                        Description = item.Specialization.Description
                    };

                    physi.Add(phys);
                }
                TempData["physList"] = physi;
                TempData.Keep();
                return View(physi);


            }
            else
            {
                List<Physician> physi = new List<Physician>();
               
                var physlist = ur.GetPhysician();

                foreach (var item in physlist)
                {
                    Physician phy = new Physician();
                   
                    phy.Id = item.Id;
                    phy.FirstName = item.FirstName;
                    phy.MiddleName = item.MiddleName;
                    phy.LastName = item.LastName;
                    phy.BirthDate = item.BirthDate;
                    phy.Gender = item.Gender;
                    phy.Height = item.Height;
                    phy.Weight = item.Weight;
                    phy.ContactInfo = new ContactInfo {
                        PhysicianId = item.Id,
                        HomeAddress = item.ContactInformation.HomeAddress,
                        HomePhone = item.ContactInformation.HomePhone,
                        OfficeAddress = item.ContactInformation.OfficeAddress,
                        OfficePhone = item.ContactInformation.OfficePhone,
                        EmailAddress = item.ContactInformation.EmailAddress,
                        CellphoneNumber = item.ContactInformation.CellphoneNumber,
                       
                    };
                    phy.Specialization = new Specialization
                    {
                        PhysicianId = item.Id,
                        Name = item.Specialization.Name,
                        Description = item.Specialization.Description
                    };
                    // physician.ContactInfo.HomePhone = item.ContactInformation.HomePhone;
                    //  physician.ContactInfo.OfficeAddress = item.ContactInformation.OfficeAddress;
                    // physician.ContactInfo.OfficePhone = item.ContactInformation.OfficePhone;
                    //  physician.ContactInfo.EmailAddress = item.ContactInformation.EmailAddress;
                    //   physician.ContactInfo.CellphoneNumber = item.ContactInformation.CellphoneNumber;
                    //  physician.Specialization.Name = item.Specialization.Name;
                    //  physician.Specialization.Description = item.Specialization.Description;
          
                    physi.Add(phy);


                }
                TempData["physicianList"] = physi;
                TempData.Keep();
                return View(physi);
            }
               
        
        }



        public ActionResult ViewContactInfo()

        {


            if (TempData["search"] != null)
            {
               
                //  var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.MiddleName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.LastName.ToLower().Contains(TempData["search"].ToString().ToLower()));
                var physList = TempData["physList"];
                TempData.Keep();
                  return View(physList);


            }
            var physicianList = TempData["physicianList"];
            TempData.Keep();
            return View(physicianList);



        }

        public ActionResult ViewSpecialization()
        {

            if (TempData["search"] != null)
            {

                //   var searchPhysician = physicians.Where(s => s.FirstName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.MiddleName.ToLower().Contains(TempData["search"].ToString().ToLower()) || s.LastName.ToLower().Contains(TempData["search"].ToString().ToLower()));

                var physList = TempData["physList"];
                TempData.Keep();
                return View(physList);
                //  return View(searchPhysician);


            }

            var physicianList = TempData["physicianList"];
            TempData.Keep();
            return View(physicianList);

        }

        // GET: Physician/Details/5
      //  public ActionResult Details(int id)
       // {
            //return View(physicians);
       // }

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


                var lastPhysicianId = ur.GetPhysician().Any(phys => phys.Id >= 1);

                if (nextBtn != null)
                {

                    if (ModelState.IsValid)
                    {
                        if (lastPhysicianId)
                        {
                            physician.Id = ur.GetPhysician().Max(item => item.Id) + 1;
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
                        ur.AddPhysician(phys.ContactInfo.HomeAddress, phys.ContactInfo.HomePhone,
                            phys.ContactInfo.OfficeAddress, phys.ContactInfo.OfficePhone, 
                            phys.ContactInfo.EmailAddress, phys.ContactInfo.CellphoneNumber, 
                            phys.Specialization.Name, phys.Specialization.Description, 
                            phys.Id, phys.FirstName, phys.MiddleName,
                            phys.LastName, phys.BirthDate, phys.Gender, phys.Weight, phys.Height);
                       
                        //physicians.Add(phys);

                        cont = new ContactInfo();
                        spec = new Specialization();
                        return RedirectToAction("Index", "Physician");
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

            var phy = ur.GetPhysicianById(id);
            Physician phys = new Physician();
            phys.FirstName = phy.FirstName;
            phys.MiddleName = phy.MiddleName;
            phys.LastName = phy.LastName;
            phys.BirthDate = phy.BirthDate;
            phys.Gender = phy.Gender;
            phys.Height = phy.Height;
            phys.Weight = phy.Weight;
            phys.ContactInfo = new ContactInfo
            {
                HomeAddress = phy.ContactInformation.HomeAddress,
                HomePhone = phy.ContactInformation.HomePhone,
                OfficeAddress = phy.ContactInformation.OfficeAddress,
                OfficePhone = phy.ContactInformation.OfficePhone,
                EmailAddress = phy.ContactInformation.EmailAddress,
                CellphoneNumber = phy.ContactInformation.CellphoneNumber
            };
            phys.Specialization = new Specialization
            {
               
                Name = phy.Specialization.Name,
                Description = phy.Specialization.Description
            };
            return View(phys);

        }

        // POST: Physician/Edit/5
        [HttpPost]
        public ActionResult EditPhysician(int id, Physician phy)
        {
            try
            {
                Physician phys = new Physician();
                phys.Id = phy.Id;
                phys.FirstName = phy.FirstName;
                phys.MiddleName = phy.MiddleName;
                phys.LastName = phy.LastName;
                phys.BirthDate = phy.BirthDate;
                phys.Gender = phy.Gender;
                phys.Height = phy.Height;
                phys.Weight = phy.Weight;
                phys.ContactInfo = new ContactInfo
                {
                    HomeAddress = phy.ContactInfo.HomeAddress,
                    HomePhone = phy.ContactInfo.HomePhone,
                    OfficeAddress = phy.ContactInfo.OfficeAddress,
                    OfficePhone = phy.ContactInfo.OfficePhone,
                    EmailAddress = phy.ContactInfo.EmailAddress,
                    CellphoneNumber = phy.ContactInfo.CellphoneNumber
                };
                phys.Specialization = new Specialization
                {

                    Name = phy.Specialization.Name,
                    Description = phy.Specialization.Description
                };

                if (TryUpdateModel(phy))
                {


                    int Retval = ur.UpdatePhysician(phys.ContactInfo.HomeAddress, phys.ContactInfo.HomePhone,
                                phys.ContactInfo.OfficeAddress, phys.ContactInfo.OfficePhone,
                                phys.ContactInfo.EmailAddress, phys.ContactInfo.CellphoneNumber,
                                phys.Specialization.Name, phys.Specialization.Description,
                                phys.Id, phys.FirstName, phys.MiddleName,
                                phys.LastName, phys.BirthDate, phys.Gender, phys.Weight, phys.Height);
                    if (Retval > 0)
                    {

                        return RedirectToAction("Index");
                    }
                }
                    return View();
                }
            

            catch
            {
                return View();
            }
       
        }

        // GET: Physician/Delete/5
        public ActionResult DeletePhysician(int id)
        {
            var phy = ur.GetPhysicianById(id);
            Physician phys = new Physician();
            phys.Id = phy.Id;
            phys.FirstName = phy.FirstName;
            phys.MiddleName = phy.MiddleName;
            phys.LastName = phy.LastName;
            phys.BirthDate = phy.BirthDate;
            phys.Gender = phy.Gender;
            phys.Height = phy.Height;
            phys.Weight = phy.Weight;
            phys.ContactInfo = new ContactInfo
            {
                HomeAddress = phy.ContactInformation.HomeAddress,
                HomePhone = phy.ContactInformation.HomePhone,
                OfficeAddress = phy.ContactInformation.OfficeAddress,
                OfficePhone = phy.ContactInformation.OfficePhone,
                EmailAddress = phy.ContactInformation.EmailAddress,
                CellphoneNumber = phy.ContactInformation.CellphoneNumber
            };
            phys.Specialization = new Specialization
            {

                Name = phy.Specialization.Name,
                Description = phy.Specialization.Description
            };
            return View(phys);
        }

        // POST: Physician/Delete/5
        [HttpPost]
        public ActionResult DeletePhysician(int id, Physician phy)
        {
            try
            {
               
                int retval = ur.DeletePhysicianById(id);
                if (retval > 0)
                {

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }

        }

       // public static List<Physician> physicians = new List<Physician>()
      //  {
                    //new Physician
                    //{
                    //    Id = 1, FirstName = "A", MiddleName = "B", LastName="C", BirthDate= DateTime.Now, Gender="Male", Height= 171, Weight= 65,
                    //    ContactInfo = new ContactInfo {PhysicianId = 1, HomeAddress= "Address", HomePhone = "099999", OfficeAddress="Office Address", OfficePhone= "0999", CellphoneNumber="0999", EmailAddress= "michael.dionglay@pointwest.com.ph"  }
                    //    , Specialization = new Specialization { PhysicianId =1, Name="Opthalmologist", Description = "Opthalmologist Description"} },

            //         new Physician
            //        {
            //            Id = 2, FirstName = "B", MiddleName = "C", LastName="D", BirthDate= DateTime.Now, Gender="Male", Height= 171, Weight= 65,
            //            ContactInfo = new ContactInfo {PhysicianId = 2, HomeAddress= "Address", HomePhone = "099999", OfficeAddress="Office Address", OfficePhone= "0999", CellphoneNumber="0999", EmailAddress= "michael.dionglay@pointwest.com.ph"  }
            //            , Specialization = new Specialization { PhysicianId =2, Name="Opthalmologist", Description = "Opthalmologist Description"} },

            //         new Physician
            //        {
            //            Id = 3, FirstName = "D", MiddleName = "E", LastName="A", BirthDate= DateTime.Now, Gender="Male", Height= 171, Weight= 65,
            //            ContactInfo = new ContactInfo {PhysicianId = 3, HomeAddress= "Address", HomePhone = "099999", OfficeAddress="Office Address", OfficePhone= "0999", CellphoneNumber="0999", EmailAddress= "michael.dionglay@pointwest.com.ph"  }
            //            , Specialization = new Specialization { PhysicianId =3, Name="Opthalmologist", Description = "Opthalmologist Description"} }

       // };

    //    static List<Physician> physicians = new List<Physician>() ;






    }


}
