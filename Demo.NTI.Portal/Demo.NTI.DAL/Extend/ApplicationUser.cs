using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.DAL.Extend
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
        }

        public bool IsActive { get; set; }
        public bool IsAgree { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
