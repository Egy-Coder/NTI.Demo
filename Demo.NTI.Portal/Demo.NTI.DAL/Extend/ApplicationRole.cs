using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.DAL.Extend
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
        }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        //[ForeignKey("CreatedBy")]
        //public ApplicationUser ApplicationUser { get; set; }
    }
}
