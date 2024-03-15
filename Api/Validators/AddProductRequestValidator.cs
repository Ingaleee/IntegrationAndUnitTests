using FluentValidation;
using OzonGrpc.ProductService.Api;

namespace Api.Validators;

public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(r => r.Name).NotNull().MinimumLength(3);
        RuleFor(r => r.Price).NotNull().NotEqual(0);
        RuleFor(r => r.Category).NotNull();
        RuleFor(r => r.Weight).NotEqual(0);
        RuleFor(r => r.WarehouseId).NotEqual(0);
    }
}