using OzonGrpc.Api.Dto;

namespace OzonGrpc.Api.Services;

public interface IProductService
{
    ulong Add(CreateProductDto dto);
    bool Update(UpdateProductDto dto);
    ICollection<GetProductDto> Get();
    GetProductDto GetById(ulong id);
}