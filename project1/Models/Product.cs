using System;
using System.Collections.Generic;

#nullable disable

namespace project1.Models
{
    public partial class Product
    {
        //public Product()
        //{
        //    OrderDetails = new HashSet<OrderDetail>();
        //}

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? QuantityInStock { get; set; }

        // why is this here? product shouldn't have a list of orders, but the other way around
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
