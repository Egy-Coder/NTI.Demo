using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.DAL.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? CityId { get; set; }
        public string? CvName { get; set; }
        public string? ImageName { get; set; }


        // Navigation Property
        public Department? Department { get; set; }
        public City? City { get; set; }

    }
}
