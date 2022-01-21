using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AM.UMS.BackEnd.Data
{
    public class ItemOrder
    {
        [Key]
        public int ItemOrderID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }
    }
}
