using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.DAL.Entities
{

    [Table("Cities")]
    public class City :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }

    }
}
