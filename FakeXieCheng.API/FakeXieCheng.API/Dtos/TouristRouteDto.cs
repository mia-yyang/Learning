﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXieCheng.API.Dtos
{
    public class TouristRouteDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double? DiscountPresent { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string Features { get; set; }
        public string Fees { get; set; }
        public string Notes { get; set; }
        public ICollection<TouristRoutePictureDto> TouristRoutePictures { get; set; }
    }
}
