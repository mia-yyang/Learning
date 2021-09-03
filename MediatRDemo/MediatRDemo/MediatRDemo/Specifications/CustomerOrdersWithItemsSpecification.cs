using Ardalis.Specification;
using MediatRDemo.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRDemo.Specifications
{
    public class CustomerOrdersWithItemsSpecification : Specification<Order>
    {
        public CustomerOrdersWithItemsSpecification(string buyerId)
        {
            Query.Where(o => o.BuyerId == buyerId)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.ItemOrdered);
        }
    }
}
