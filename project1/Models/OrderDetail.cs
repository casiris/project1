using System;
using System.Collections.Generic;

#nullable disable

namespace project1.Models
{
    public partial class OrderDetail
    {
        public int DetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? QuantityOrdered { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }  // i don't know why this is here, since productId should be the reference to the product
    }
}
