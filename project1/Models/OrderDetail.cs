using System;
using System.Collections.Generic;

#nullable disable

namespace project1.Models
{
    public partial class OrderDetail
    {
        public int DetailsId { get; set; }
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public int? QuantityOrdered { get; set; }

        //public virtual Order Order { get; set; }
        //public virtual Product Product { get; set; }  // i don't know why this is here, since productId should be the reference to the product

        // create an object for each element in the inputted OrderDetails list, then insert them into the db
        public void AddOrderDetails(int oID, List<OrderDetail> products)
        {
            shoppingAppContext db = new shoppingAppContext();

            foreach (var item in products)
            {
                var itemDetail = new OrderDetail();
                itemDetail.OrderId = oID;
                itemDetail.ProductId = item.ProductId;
                itemDetail.QuantityOrdered = item.QuantityOrdered;

                db.Add(itemDetail);
                db.SaveChanges();
            }
        }
    }
}
