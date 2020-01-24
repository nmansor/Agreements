using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }

        public string IdentificationId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
