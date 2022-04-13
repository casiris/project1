using Microsoft.AspNetCore.Mvc;
using project1.Models;
using System;
using System.Collections.Generic;

namespace project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        Order orderModel = new Order();
        OrderDetail orderDetailModel = new OrderDetail();

        [HttpPost]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(int customerId, List<OrderDetail> products)
        {
            // first i need to create the order in the database, because i'll need to orderID to pass to the orderDetail that is created next
            int newOrderId = 0;

            try
            {
                newOrderId = orderModel.PlaceOrder(customerId, products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            // now create the orderDetails
            try
            {
                orderDetailModel.AddOrderDetails(newOrderId, products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("OrderHistory")]
        public IActionResult OrderHistoryByCustomer(int customerID)
        {
            if (orderModel.OrderHistoryByCustomer(customerID) == null)
            {
                return BadRequest("Invalid customer ID");
            }
            return Ok(orderModel.OrderHistoryByCustomer(customerID));
        }

        [HttpGet]
        [Route("OpenOrders")]
        public IActionResult OpenOrders()
        {
            return Ok(orderModel.OpenOrders());
        }

        [HttpGet]
        [Route("Invoice")]
        public IActionResult GetInvoice(int customerID)
        {
            if (orderModel.GetInvoice(customerID) == null)
            {
                return BadRequest("Invalid customer ID");
            }
            return Ok(orderModel.GetInvoice(customerID));
        }
    }
}