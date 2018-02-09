using System;

namespace Web.Models
{
    public class EidCard
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostal { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime ValidityBegin { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string POB { get; set; }
        public string CardNumber { get; set; }
        public string ChipNumber { get; set; }
        public string DocumentType { get; set; }
        public string NobleCondition { get; set; }
        public string CardDeliveryMunicipality { get; set; }
        public string Nationality { get; set; }
        public string RNN { get; set; }
        public string CertificateAuth { get; set; }
    }
}