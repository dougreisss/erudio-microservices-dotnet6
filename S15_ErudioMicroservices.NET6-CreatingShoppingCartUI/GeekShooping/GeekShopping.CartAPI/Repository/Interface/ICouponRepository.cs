using GeekShopping.CartAPI.Data.ValueObjects;
using System.Threading.Tasks;

namespace GeekShopping.CartAPI.Repository.Interface
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode, string token);
    }
}
