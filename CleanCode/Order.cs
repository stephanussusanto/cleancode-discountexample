using System;
using System.Collections.Generic;
using CleanCode.Origin;

namespace CleanCode
{
    public class Order
    {
        public List<OrderDetail> OrderDetails { get; set; }

        public Customer Customer { get; set; }
    }
}
