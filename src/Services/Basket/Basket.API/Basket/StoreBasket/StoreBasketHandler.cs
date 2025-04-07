

using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler
    (  IBasketRepository basketRepository ,DiscountProtoService.DiscountProtoServiceClient discountproto   )
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscunt(command.Cart, cancellationToken).ConfigureAwait(false);


        await basketRepository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscunt(ShoppingCart cart,CancellationToken cancellationToken) {
        foreach (var item in   cart.Items)
        {
            var coupon = await discountproto.GetDiscountAsync(new GetDiscountRequest
            {
                ProductName = item.ProductName,

            }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
     
}
