using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CouponVO>> FindById(string couponCode)
        {
            var copoun = await _couponRepository.GetCouponByCouponCode(couponCode);

            if (copoun == null) return NotFound();

            return Ok(copoun);
        }
    }
}
