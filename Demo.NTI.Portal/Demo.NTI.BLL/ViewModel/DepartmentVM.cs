using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.ViewModel
{
    public class DepartmentVM : BaseVM
    {
        public DepartmentVM()
        {
            IsActive = true;
            CreatedAt = DateTime.Now;
            UpdateFlag = false;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
