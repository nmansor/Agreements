using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Notarizer
    {
        public Notarizer()
        {
            QulificationDocuments = new HashSet<QualificationDocument>();
            Agreements = new HashSet<Agreement>();
        }
        public int NotarizerId { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        // [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        public string LandPhone { get; set; }
        public string NotarizerJudicialNumber { get; set; }

        // [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be loner then 100 characters")]
        public short? CityId { get; set; }
        public short? CourtId { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string FamilyName { get; set; }
        public string MobilePhone { get; set; }
        public DateTime DateRegistered { get; set; }
        public NotarizerStatusEnum Status { get; set; }
        public bool IssuedStamp { get; set; }
        public DateTime? DateApproved { get; set; }

        public byte[] Stamp { get; set; }
        public virtual CourtLookUp Court { get; set; }
        public virtual CityLookUp City { get; set; }

        public virtual ICollection<QualificationDocument> QulificationDocuments { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
       
    }
}
