using Api.Filters;
using OzonGrpc.ProductService.Api;
using ProductCategory = OzonGrpc.Domain.ProductCategory;

namespace Api.Extensions;

public static class ListProductQueryRequestExtensions
{
    public static ListProductQuery ToQuery(this ListProductQueryRequest request)
    {
        return new ListProductQuery
        {
            CreatedUtc = request.CreatedUtc != default ? request.CreatedUtc.ToDateTime() : null,
            Category = Enum.TryParse(typeof(ProductCategory), request.Category, out var category)
                ? (ProductCategory)category
                : null,
            WarehouseId = request.WarehouseId != 0 ? request.WarehouseId : null,
            Skip = request.Skip,
            Take = request.Take != 0 ? request.Take : null
        };
    }
}