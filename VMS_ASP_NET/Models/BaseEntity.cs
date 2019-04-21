using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMS_ASP_NET.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
     
        [ScaffoldColumn(false)]
        public int CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }
        [ScaffoldColumn(false)]
        public int ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedOn { get; set; }

    }
}
