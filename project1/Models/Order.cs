using System;
using System.Collections.Generic;
using System.Linq;

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
        //public virtual Customer Customer { get; set; }

        // one order can have many orderDetails
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        shoppingAppContext db = new shoppingAppContext();

        // return the orderID after insertion, so that the orderDetails can use it when it's created
        public int PlaceOrder(int customerId, List<OrderDetail> products)
        {
            Order order = new Order();
            order.DateOrdered = DateTime.Now;
            order.CustomerId = customerId;

            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Invalid customer ID");
            }

            // apparently, with linq, when an insertion is done, the values are returned to the object
            // so here, i can get the id of the newly created order very easily
            return order.OrderId;
        }

        public IQueryable OrderHistoryByCustomer(int id)
        {
            //SELECT Orders.orderID, dateOrdered, customerID, productName, quantityOrdered, price
            //FROM Orders
            //JOIN OrderDetails ON Orders.orderID = OrderDetails.orderID
            //JOIN Products ON Products.productID = OrderDetails.productID
            //WHERE customerID = 1;

            var orderHistory = (from order in db.Orders
                                join orderDetail in db.OrderDetails on order.OrderId equals orderDetail.OrderId
                                join products in db.Products on orderDetail.ProductId equals products.ProductId
                                where order.CustomerId == id
                                select new
                                {
                                    OrderID = order.OrderId,
                                    DateOrdered = order.DateOrdered,
                                    CustomerId = order.CustomerId,
                                    ProductName = products.ProductName,
                                    QuantityOrdered = orderDetail.QuantityOrdered,
                                    Price = products.Price
                                });

            // linq query will at the very least return an empty list/iqueryable if it doesn't find the entry in the database
            // so to validate that, instead i'll check if it's empty, and if it is, return null and handle that exception
            if (orderHistory.ToList().Count() < 1)
            {
                return null;
            }

            return orderHistory;
        }

        public string OpenOrders()
        {
            //SELECT Orders.orderID, dateOrdered, customerID, productName, quantityOrdered, price FROM Orders
            //JOIN OrderDetails ON orders.orderID = OrderDetails.orderID
            //JOIN Products ON OrderDetails.productID = Products.productID
            //WHERE dateOrdered >= DATEADD(day, -1, GETDATE());

            string outputString = "";
            DateTime lastDay = DateTime.Now.AddDays(-1);

            var openOrders = (from order in db.Orders
                              join orderDetail in db.OrderDetails on order.OrderId equals orderDetail.OrderId
                              join products in db.Products on orderDetail.ProductId equals products.ProductId
                              where order.DateOrdered >= lastDay
                              select new
                              {
                                  OrderID = order.OrderId,
                                  DateOrdered = order.DateOrdered,
                                  CustomerID = order.CustomerId,
                                  ProductName = products.ProductName,
                                  QuantityOrdered = orderDetail.QuantityOrdered,
                                  Price = products.Price
                              });

            foreach (var i in openOrders)
            {
                outputString += i.ToString() + "\n\n";
            }

            return outputString;
        }

        public string GetInvoice(int id)
        {
            double total = 0;
            string outputString = "";

            //SELECT productName, price, quantityOrdered, customerID
            //FROM Orders
            //JOIN OrderDetails ON Orders.orderID = OrderDetails.orderID
            //JOIN Products ON OrderDetails.productID = Products.productID
            //WHERE customerID = 1;

            DateTime lastDay = DateTime.Now.AddDays(-1);

            var invoice = (from order in db.Orders
                          join orderDetails in db.OrderDetails on order.OrderId equals orderDetails.OrderId
                          join products in db.Products on orderDetails.ProductId equals products.ProductId
                          where (order.CustomerId == id) && (order.DateOrdered >= lastDay)
                          select new
                          {
                              ProductName = products.ProductName,
                              Price = products.Price,
                              QuantityOrdered = orderDetails.QuantityOrdered,
                              CustomerID = order.CustomerId
                          });

            // once again, if the entered customer id isn't found, return null
            if (invoice.ToList().Count() < 1)
            {
                return null;
            }

            foreach (var i in invoice)
            {
                total += Convert.ToDouble(i.Price) * Convert.ToDouble(i.QuantityOrdered);
                outputString += i.ToString() + "\n\n";
            }

            return outputString + "\n\nThe total for the entire order is: " + String.Format("{0:C2}", total);
        }
    }
}
