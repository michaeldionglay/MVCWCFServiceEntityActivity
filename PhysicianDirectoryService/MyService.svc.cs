using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;



namespace PhysicianDirectoryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    public class MyService : IMyService
    {
        public void DoWork()
        {
        }


        public List<Physician> GetPhysician()
        {
            List<Physician> physlist = new List<Physician>();
            PhysicianDBEntities tstDb = new PhysicianDBEntities();
            var lstUsr = from k in tstDb.Physicians select k;
            foreach (var item in lstUsr)
            {
                Physician phys = new Physician();
                phys.Id = item.Id;
                phys.FirstName = item.FirstName;
                phys.MiddleName = item.MiddleName;
                phys.LastName = item.LastName;
                phys.BirthDate = item.BirthDate;
                phys.Gender = item.Gender;
                phys.Weight = item.Weight;
                phys.Height = item.Height;
                phys.ContactInformation = new ContactInformation
                {
                    HomeAddress = item.ContactInformation.HomeAddress,
                    HomePhone = item.ContactInformation.HomePhone,
                    OfficeAddress = item.ContactInformation.OfficeAddress,
                    OfficePhone = item.ContactInformation.OfficePhone,
                    EmailAddress = item.ContactInformation.EmailAddress,
                    CellphoneNumber = item.ContactInformation.CellphoneNumber
                };
                phys.Specialization = new Specialization
                {
                    Name = item.Specialization.Name,
                    Description = item.Specialization.Description
                };
                // phys.Specialization = item.Specialization;
                physlist.Add(phys);

            }

            return physlist;
        }



        public Physician GetPhysicianById(int id)
        {

            PhysicianDBEntities tstDb = new PhysicianDBEntities();
            var lstUsr = from k in tstDb.Physicians where k.Id == id select k;
            Physician phys = new Physician();
            foreach (var item in lstUsr)
            {

                phys.Id = item.Id;
                phys.FirstName = item.FirstName;
                phys.MiddleName = item.MiddleName;
                phys.LastName = item.LastName;
                phys.BirthDate = item.BirthDate;
                phys.Gender = item.Gender;
                phys.Weight = item.Weight;
                phys.Height = item.Height;
                phys.ContactInformation = new ContactInformation
                {
                    Id = item.Id,
                    HomeAddress = item.ContactInformation.HomeAddress,
                    HomePhone = item.ContactInformation.HomePhone,
                    OfficeAddress = item.ContactInformation.OfficeAddress,
                    OfficePhone = item.ContactInformation.OfficePhone,
                    EmailAddress = item.ContactInformation.EmailAddress,
                    CellphoneNumber = item.ContactInformation.CellphoneNumber
                };
                phys.Specialization = new Specialization
                {
                    Id = item.Id,
                    Name = item.Specialization.Name,
                    Description = item.Specialization.Description
                };



            }

            return phys;
        }

        public int DeletePhysicianById(int Id)
        {

            PhysicianDBEntities tstDb = new PhysicianDBEntities();
            Specialization spec = new Specialization();
            ContactInformation cont = new ContactInformation();
            Physician phys = new Physician();
            spec.Id = Id;
            cont.Id = Id;
            phys.Id = Id;
            tstDb.Entry(spec).State = EntityState.Deleted;
            tstDb.Entry(cont).State = EntityState.Deleted;
            tstDb.Entry(phys).State = EntityState.Deleted;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int AddPhysician(string HomeAddress, string HomePhone, string OfficeAddress, string OfficePhone, string EmailAddress, string CellphoneNumber, string Name, string Description, int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, double? Weight, double? Height)
        {
            PhysicianDBEntities db = new PhysicianDBEntities();
            Physician phys = new Physician();
            phys.Id = Id;
            phys.FirstName = FirstName;
            phys.MiddleName = MiddleName;
            phys.LastName = LastName;
            phys.BirthDate = BirthDate;
            phys.Gender = Gender;
            phys.Weight = Weight;
            phys.Height = Height;
            phys.ContactInformation = new ContactInformation
            {
                Id = Id,
                HomeAddress = HomeAddress,
                HomePhone = HomePhone,
                OfficeAddress = OfficeAddress,
                OfficePhone = OfficePhone,
                EmailAddress = EmailAddress,
                CellphoneNumber = CellphoneNumber
            };
            phys.Specialization = new Specialization
            {
                Id = Id,
                Name = Name,
                Description = Description
            };
            db.Physicians.Add(phys);
            int Retval = db.SaveChanges();
            return Retval;
        }
        public int UpdatePhysician(string HomeAddress, string HomePhone, string OfficeAddress, string OfficePhone, string EmailAddress, string CellphoneNumber, string Name, string Description, int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, double? Weight, double? Height)
        {
            PhysicianDBEntities db = new PhysicianDBEntities();
            Physician phys = new Physician();
            ContactInformation cont = new ContactInformation();
            Specialization spec = new Specialization();
            phys.Id = Id;
            phys.FirstName = FirstName;
            phys.MiddleName = MiddleName;
            phys.LastName = LastName;
            phys.BirthDate = BirthDate;
            phys.Gender = Gender;
            phys.Weight = Weight;
            phys.Height = Height;
            cont.Id = Id;
            cont.HomeAddress = HomeAddress;
            cont.HomePhone = HomePhone;
            cont.OfficeAddress = OfficeAddress;
            cont.OfficePhone = OfficePhone;
            cont.EmailAddress = EmailAddress;
            cont.CellphoneNumber = CellphoneNumber; 
            spec.Id = Id;
            spec.Name = Name;
            spec.Description = Description;
            //phys.ContactInformation = new ContactInformation
            //{
            //     Id = Id,
            //    HomeAddress = HomeAddress,
            //    HomePhone = HomePhone,
            //    OfficeAddress = OfficeAddress,
            //    OfficePhone = OfficePhone,
            //    EmailAddress = EmailAddress,
            //    CellphoneNumber = CellphoneNumber
            //};
            //phys.Specialization = new Specialization
            //{
            //     Id = Id,
            //    Name = Name,
            //    Description = Description
            //};

            db.Entry(phys).State = EntityState.Modified;
            db.Entry(cont).State = EntityState.Modified;
            db.Entry(spec).State = EntityState.Modified;
            int Retval = db.SaveChanges();
            return Retval;
        }

        public List<Physician> GetPhysicianByName(string name)
        {
            List<Physician> physlist = new List<Physician>();
            PhysicianDBEntities tstDb = new PhysicianDBEntities();

            var list = from k in tstDb.Physicians select k;
            if (name == String.Empty)
            {
                var listPhys = from k in tstDb.Physicians select k;
                list = listPhys;

            }
            else
            {
                var listPhys = from k in tstDb.Physicians where k.FirstName.ToLower().Contains(name.ToLower()) || k.MiddleName.ToLower().Contains(name.ToLower()) || k.LastName.ToLower().Contains(name.ToLower()) select k;
                list = listPhys;
            }
         
            foreach (var item in list)
            {
                Physician phys = new Physician();
                phys.Id = item.Id;
                phys.FirstName = item.FirstName;
                phys.MiddleName = item.MiddleName;
                phys.LastName = item.LastName;
                phys.BirthDate = item.BirthDate;
                phys.Gender = item.Gender;
                phys.Weight = item.Weight;
                phys.Height = item.Height;
                phys.ContactInformation = new ContactInformation
                {
                    Id = item.Id,
                    HomeAddress = item.ContactInformation.HomeAddress,
                    HomePhone = item.ContactInformation.HomePhone,
                    OfficeAddress = item.ContactInformation.OfficeAddress,
                    OfficePhone = item.ContactInformation.OfficePhone,
                    EmailAddress = item.ContactInformation.EmailAddress,
                    CellphoneNumber = item.ContactInformation.CellphoneNumber
                };
                phys.Specialization = new Specialization
                {
                    Id = item.Id,
                    Name = item.Specialization.Name,
                    Description = item.Specialization.Description
                };

                physlist.Add(phys);

            }

            return physlist;
        }
    }
}

