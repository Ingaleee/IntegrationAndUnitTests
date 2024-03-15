using FluentValidation;
using OzonGrpc.ProductService.Api;

namespace Api.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(r => r.Id).NotEqual(0ul);
        RuleFor(r => r.WarehouseId).NotEqual(0);
        RuleFor(r => r.Name).MinimumLength(3);
        RuleFor(r => r.Weight).NotEqual(0f);
    }
}