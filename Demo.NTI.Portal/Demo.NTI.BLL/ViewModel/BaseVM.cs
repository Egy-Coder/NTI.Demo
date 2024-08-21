using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.ViewModel
{
    public class BaseVM
    {
        public bool? IsActive { get; set; }
        public bool? UpdateFlag { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
