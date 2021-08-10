using FakeXieCheng.API.Database;
using FakeXieCheng.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXieCheng.API.Services
{
    public class TouristRouteRepostitory : ITouristRouteRepostitory
    {
        private readonly AppDbContext _context;
        public TouristRouteRepostitory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync(
            string keyword,
            string operatorType,
            int ratingValue,
            int pageSize,
            int pageNumber,
            string orderBy
        )
        {
            // return _context.TouristRoutes;
            // Include vs join
            IQueryable<TouristRoute> result = _context
                .TouristRoutes
                .Include(t => t.TouristRoutePictures);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result = result.Where(t => t.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            {
                switch (operatorType)
                {
                    case "largerThan":
                        result = result.Where(t => t.Rating >= ratingValue);
                        break;
                    case "lessThan":
                        result = result.Where(t => t.Rating <= ratingValue);
                        break;
                    case "equalTo":
                        result = result.Where(t => t.Rating == ratingValue);
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                // ToLowerInvariant 只处理英语
                if (orderBy.ToLowerInvariant() == "originalprice")
                {
                    result = result.OrderBy(t => t.OriginalPrice);
                }
            }

            return await result.ToListAsync();// 这里转成 ToList()的作用 IQueryable 马上执行  类似功能的还有FirstOrDefault
        }

        public async Task<TouristRoute> GetTouristRouteAsync(Guid touristRouteId)
        {
            // return _context.TouristRoutes.FirstOrDefault(n => n.Id == touristRouteId);

            return await _context.TouristRoutes.Include(t => t.TouristRoutePictures).FirstOrDefaultAsync(n => n.Id == touristRouteId);
        }

        public async Task<bool> TouristRouteExistsAsync(Guid touristRouteId)
        {
            return await _context.TouristRoutes.AnyAsync(n => n.Id == touristRouteId);
        }

        public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIdAsync(Guid touristRouteId)
        {
            return await _context.TouristRoutePictures.Where(p => p.TouristRouteId == touristRouteId).ToListAsync();
        }

        public async Task<TouristRoutePicture> GetPictureAsync(int pictureId)
        {
            return await _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDListAsync(IEnumerable<Guid> ids)
        {
            return await _context.TouristRoutes.Where(t => ids.Contains(t.Id)).ToListAsync();
        }

        public void AddTouristRoute(TouristRoute touristRoute)
        {
            if (touristRoute == null)
            {
                throw new ArgumentNullException(nameof(touristRoute));
            }

            _context.TouristRoutes.Add(touristRoute);
        }

        public void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture)
        {
            if (touristRouteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(touristRouteId));
            }
            if (touristRoutePicture == null)
            {
                throw new ArgumentNullException(nameof(touristRoutePicture));
            }
            touristRoutePicture.TouristRouteId = touristRouteId;
            _context.TouristRoutePictures.Add(touristRoutePicture);
        }

        public void DeleteTouristRoute(TouristRoute touristRoute)
        {
            _context.TouristRoutes.Remove(touristRoute);
        }

        public void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes)
        {
            _context.TouristRoutes.RemoveRange(touristRoutes);
        }

        public void DeleteTouristRoutePicture(TouristRoutePicture touristRoutePicture)
        {
            _context.TouristRoutePictures.Remove(touristRoutePicture);
        }

        public async Task<ShoppingCart> GetShoppingCartByUserId(string userId)
        {
            return await _context.ShoppingCarts
                .Include(s => s.User)
                .Include(s => s.ShoppingCartItems).ThenInclude(li => li.TouristRoute)
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateShoppingCart(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }

        public async Task AddShoppingCartItem(LineItem lineItem)
        {
            await _context.LineItems.AddAsync(lineItem);
        }

        public async Task<LineItem> GetShoppingCartItemByUserId(int lineItemId)
        {
            return await _context.LineItems
              .Where(s => s.Id == lineItemId)
              .FirstOrDefaultAsync();
        }

        public void DeleteShoppingCartItem(LineItem lineItem)
        {
            _context.LineItems.Remove(lineItem);
        }

        public async Task<IEnumerable<LineItem>> GetShoppingCartItemByIdListAsync(IEnumerable<int> ids)
        {
            return await _context.LineItems
             .Where(s => ids.Contains(s.Id))
             .ToListAsync();
        }

        public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems)
        {
            _context.LineItems.RemoveRange(lineItems);
        }


    }
}
