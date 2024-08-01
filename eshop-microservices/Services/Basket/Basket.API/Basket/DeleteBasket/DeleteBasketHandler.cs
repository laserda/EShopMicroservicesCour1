﻿
namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UseName is required");
    }
}
public class DeleteBasketCommandHandler(IBasketRepository _basketRepository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(command.UserName, cancellationToken);

        return new DeleteBasketResult(true);
    }
}

