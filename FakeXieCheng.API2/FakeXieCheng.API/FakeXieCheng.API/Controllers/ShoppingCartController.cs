using AutoMapper;
using FakeXieCheng.API.Dtos;
using FakeXieCheng.API.Helper;
using FakeXieCheng.API.Models;
using FakeXieCheng.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FakeXieCheng.API.Controllers
{
    [ApiController]
    [Route("api/shoppingCart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITouristRouteRepostitory _touristRouteRepostitory;
        private readonly IMapper _mapper;
        public ShoppingCartController(
            IHttpContextAccessor httpContextAccessor,
            ITouristRouteRepostitory touristRouteRepostitory
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _touristRouteRepostitory = touristRouteRepostitory;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingCart()
        {
            // 1.获取当前用户
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // 2.使用userId获取购物车
            var shoppingCart = await _touristRouteRepostitory.GetShoppingCartByUserId(userId);

            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddShoppingCartItem(
            [FromBody] AddShoppingCartItemDto addShoppingCartItemDto
        )
        {
            // 1.获取当前用户
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // 2.使用userId获取购物车
            var shoppingCart = await _touristRouteRepostitory.GetShoppingCartByUserId(userId);

            // 3.创建lineItem
            var touristRoute = await _touristRouteRepostitory
                .GetTouristRouteAsync(addShoppingCartItemDto.TouristRouteId);
            if (touristRoute == null)
            {
                return NotFound("旅游路线不存在");
            }

            var lineItem = new LineItem()
            {
                TouristRouteId = addShoppingCartItemDto.TouristRouteId,
                ShoppingCartId = shoppingCart.Id,
                OriginalPrice = touristRoute.OriginalPrice,
                DiscountPresent = touristRoute.DiscountPresent
            };

            // 4.添加lineItem，并保存数据库
            await _touristRouteRepostitory.AddShoppingCartItem(lineItem);
            await _touristRouteRepostitory.SaveAsync();

            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteShoppingCartItem([FromRoute] int itemId)
        {
            // 获取lineItem数据
            var lineItem = await _touristRouteRepostitory.GetShoppingCartItemByUserId(itemId);
            if (lineItem == null)
            {
                return NotFound("购物车商品找不到");
            }

            _touristRouteRepostitory.DeleteShoppingCartItem(lineItem);
            await _touristRouteRepostitory.SaveAsync();

            return NoContent();
        }

        [HttpDelete("items/(itemIDs)")]
        public async Task<IActionResult> DeleteShoppingCartItems(
            [ModelBinder(BinderType = typeof(CustomArrayModeBinder))]
            [FromRoute] IEnumerable<int> itemIDs)
        {
            var lineItems = await _touristRouteRepostitory.GetShoppingCartItemByIdListAsync(itemIDs);

            _touristRouteRepostitory.DeleteShoppingCartItems(lineItems);
            await _touristRouteRepostitory.SaveAsync();

            return NoContent();
        }
    }
}
