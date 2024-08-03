using Discount.Grpc.Models;
using Grpc.Core;

namespace Discount.Grpc.Services;
public class DiscountService(DiscountContext discountContext,ILogger<DiscountContext> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountResquest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        discountContext.Coupones.Add(coupon);
        await discountContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountResquest request, ServerCallContext context)
    {
        var coupon = await discountContext.Coupones.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Not found"));

        discountContext.Coupones.Remove(coupon);
        await discountContext.SaveChangesAsync();

        return new DeleteDiscountResponse { Success = true };
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountResquest request, ServerCallContext context)
    {
        var coupon = await discountContext.Coupones.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        if(coupon is null)
            coupon = new Coupon {  ProductName= "No Discount", Amount=0, Description= "No Discount" };

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountResquest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
        
        discountContext.Coupones.Update(coupon);
        await discountContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
}
