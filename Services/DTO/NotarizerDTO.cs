using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTO
{

    using Common;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class NotarizerDTO
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotarizerId { get; set; }
        public int UserId { get; set; }
        //  [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string FamilyName { get; set; }

        public string NotarizerJudicialNumber { get; set; }
        public short CourtId { get; set; }
        public string Court { get; set; }
        public string CourtRegistratioNo { get; set; }
        // [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        public string LandPhone { get; set; }
        public string MobilePhone { get; set; }
        // [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be loner then 100 characters")]
        public string Address { get; set; }
        public short CityId { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public NotarizerStatusEnum Status { get; set; }

        public DateTime? DateApproved { get; set; }

        public bool IssuedStamp { get; set; }
        public IList<QualificationDocument> Documents { get; set; }

    }

}
