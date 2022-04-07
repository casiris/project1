using System;
using System.Collections.Generic;
using System.Linq;

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

        shoppingAppContext db = new shoppingAppContext();

        public bool DoesProductExist(List<OrderDetail> products)
        {
            // run a sql query to get a list of all the product ids in the table
            List<int> ids = (from p in db.Products
                            select p.ProductId).ToList();

            // then check if the product ids entered by the user are contained within the valid ids
            // if they aren't, return false
            try
            {
                foreach (var p in products)
                {
                    if (ids.Contains(p.ProductId))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                throw new Exception("Invalid product ID");
            }
            return false;
        }

        public bool HasQuantity (List<OrderDetail> products)
        {
            return true;
        }
    }
}
