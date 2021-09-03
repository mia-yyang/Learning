using MediatRDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRDemo.Entities
{
    public class CatalogBrand : BaseEntity, IAggregateRoot
    {
        public string Brand { get; private set; }
        public CatalogBrand(string brand)
        {
            Brand = brand;
        }
    }
}
