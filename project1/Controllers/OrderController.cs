using Microsoft.AspNetCore.Mvc;
using project1.Models;
using System.Collections.Generic;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        Order orderModel = new Order();
        OrderDetail orderDetailModel = new OrderDetail();

        [HttpPut]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(int customerId, List<OrderDetail> products)
        {
            // the priority of placing an order is very particular
            // first i need to validate whether or not the product id exists, and whether there's enough stock
            //Product testProduct  = new Product();
            //if (testProduct.DoesProductExist(products))
            //{

            //}
            //else
            //{
            //    throw new System.Exception("no product my guy");
            //}


            // then i need to create the order in the database, because i'll need to orderID to pass to the orderDetail that is created next
            // create the orderDetail and add it to the database
            int newOrderId = orderModel.PlaceOrder(customerId, products);
            
            // finally, call the OrderDetailsController so that it can handle the creation of the orderDetails part of the order
            // or...maybe not bother with a controller for that, because wouldn't i need a route?
            // and is a route really necessary when it's just a sub-process for the overall order?
            orderDetailModel.AddOrderDetails(newOrderId, products);

            return Ok();

            //return Created("", orderModel.PlaceOrder(customerId, products));
        }

        [HttpGet]
        [Route("OrderHistory")]
        public IActionResult OrderHistoryByCustomer(int cID)
        {
            return Ok(orderModel.OrderHistoryByCustomer(cID));
        }

        [HttpGet]
        [Route("OpenOrders")]
        public IActionResult OpenOrders()
        {
            return Ok(orderModel.OpenOrders());
        }

        [HttpGet]
        [Route("Invoice")]
        public IActionResult GetInvoice(int id)
        {
            return Ok(orderModel.GetInvoice(id));
        }
    }
}