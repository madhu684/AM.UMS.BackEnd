using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.Data
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public decimal OrderValue { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

    }
}
