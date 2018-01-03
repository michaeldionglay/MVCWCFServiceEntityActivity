using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PhysicianDirectoryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService" in both code and config file together.
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        List<Physician> GetPhysician();
        [OperationContract]
        int AddPhysician(string HomeAddress, string HomePhone, string OfficeAddress, string OfficePhone, string EmailAddress, string CellphoneNumber, string Name, string Description, int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, double? Weight, double? Height);
        [OperationContract]
        Physician GetPhysicianById(int id);

        [OperationContract]
        int UpdatePhysician(string HomeAddress, string HomePhone, string OfficeAddress, string OfficePhone, string EmailAddress, string CellphoneNumber, string Name, string Description, int Id, string FirstName, string MiddleName, string LastName, DateTime BirthDate, string Gender, double? Weight, double? Height);

        [OperationContract]
        int DeletePhysicianById(int Id);

        [OperationContract]
        List<Physician> GetPhysicianByName(string name);
    }


    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public double? Weight { get; set; }
        [DataMember]
        public double? Height { get; set; }
        [DataMember]
        public ContactInformation ContacInformation { get; set; }
        [DataMember]
        public Specialization Specialization { get; set; }


    }

}
