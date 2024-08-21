using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.DAL.Entities
{

    [Table("Countries")]
    public class Country : BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
