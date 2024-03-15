using Api.Filters;
using OzonGrpc.Api.Dto;

namespace OzonGrpc.Api.Services;

public interface IProductService
{
    ulong Add(CreateProductDto dto);
    bool Update(UpdateProductDto dto);
    IEnumerable<GetProductDto> Get(ListProductQuery query);
    GetProductDto GetById(ulong id);
}