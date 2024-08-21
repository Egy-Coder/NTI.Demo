using Demo.NTI.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.ViewModel
{
    public class EmployeeVM : BaseVM
    {

        public EmployeeVM()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
            UpdateFlag = false;
        }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "employee name required")]
        [MinLength(3,ErrorMessage = "min len 3")]
        [MaxLength(50 , ErrorMessage = "max len 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "employee email required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string Email { get; set; }

        [Range(200000 , 500000 , ErrorMessage = "salary out of range")]
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }
        public string? CvName { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Cv { get; set; }
        public IFormFile? Image { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }

    }
}
