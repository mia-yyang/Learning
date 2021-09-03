﻿using MediatRDemo.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRDemo.ViewModels
{
    public class OrderViewModel
    {
        private const string DEFAULT_STATUS = "Pending";

        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status => DEFAULT_STATUS;
        public Address ShippingAddress { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
