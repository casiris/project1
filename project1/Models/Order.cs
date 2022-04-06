using System;
using System.Collections.Generic;

#nullable disable

namespace project1.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime? DateOrdered { get; set; }
        public int? CustomerId { get; set; }

        // one order belongs to one customer
        public virtual Customer Customer { get; set; }

        // one order can have many orderDetails
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
