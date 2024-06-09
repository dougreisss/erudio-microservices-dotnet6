using GeekShopping.CouponAPI.Data.ValueObjects;

namespace GeekShopping.CouponAPI.Repository.Interface
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}
