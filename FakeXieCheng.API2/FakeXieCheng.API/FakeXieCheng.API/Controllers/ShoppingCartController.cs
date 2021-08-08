using AutoMapper;
using FakeXieCheng.API.Dtos;
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
    }
}
