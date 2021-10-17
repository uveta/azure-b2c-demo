using System.Collections.Generic;
using B2cDemo.Security;
using B2cDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2cDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policies.OrdersRead)]
        public List<OrderModel> GetOrder()
        {
            return new List<OrderModel>();
        }

        [HttpPost]
        [Authorize(Policies.OrdersFull)]
        public OrderModel CreateOrder()
        {
            return new OrderModel();
        }
    }
}
