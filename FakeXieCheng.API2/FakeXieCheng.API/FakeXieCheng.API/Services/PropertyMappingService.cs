using FakeXieCheng.API.Dtos;
using FakeXieCheng.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXieCheng.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _touristRoutePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id",new PropertyMappingValue(new List<string>(){ "Id"})},
                { "Title",new PropertyMappingValue(new List<string>(){ "Title"})},
                { "Rating",new PropertyMappingValue(new List<string>(){ "Rating"})},
                { "OriginalPrice",new PropertyMappingValue(new List<string>(){ "OriginalPrice"})}
            };

        // private IList<PropertyMapping<TSource, TDestination>> _propertyMappings;
        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(
                new PropertyMapping<TouristRouteDto, TouristRoute>(
                    _touristRoutePropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue>
           GetPropertyMapping<TSource, TDestination>()
        {
            // 获取匹配的映射对象
            var matchingMapper =
                _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapper.Count() == 1)
            {
                return matchingMapper.First()._mappingDictionary;
            }

            throw new Exception(
                $"Cannot find exact property mapping instance of <{typeof(TSource)},{typeof(TDestination)}.>");
        }
    }
}
