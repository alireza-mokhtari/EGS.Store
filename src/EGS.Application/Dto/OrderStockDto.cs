using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGS.Application.Dto
{
    public class OrderStockDto
    {
        public long OrderId { get; set; }
        public long BookId { get; set; }
        public int Stock { get; set; }
        public int OrderQuantity { get; set; }
    }
}
