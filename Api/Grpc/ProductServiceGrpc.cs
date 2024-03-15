using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonGrpc.Api.Dto;
using OzonGrpc.Api.Services;
using OzonGrpc.ProductService.Api;
using ProductCategory = OzonGrpc.Domain.ProductCategory;

namespace OzonGrpc.Api.Grpc;

public class ProductServiceGrpc : ProductService.Api.ProductService.ProductServiceBase
{
    private readonly IProductService _productService;

    public ProductServiceGrpc(IProductService productService)
    {
        _productService = productService;
    }

    public override Task<AddProductResponse> Add(AddProductRequest request, ServerCallContext context)
    {
        var dto = new CreateProductDto
        {
            Name = request.Name,
            Weight = request.Weight,
            Category = (Domain.ProductCategory)request.Category,
            WarehouseId = request.WarehouseId
        };
        var id = _productService.Add(dto);
        return Task.FromResult(new AddProductResponse { Id = id });
    }
    
    public override Task<UpdateProductResponse> Update(UpdateProductRequest request, ServerCallContext context)
    {
        var dto = new UpdateProductDto
        {
            Id = request.Id,
            Name = request.Name,
            Weight = request.Weight,
            Category = (Domain.ProductCategory?)request.Category,
            WarehouseId = request.WarehouseId
        };

        var category = (ProductCategory?)(dto.Category.HasValue
            ? (ProductService.Api.ProductCategory)dto.Category.Value
            : ProductService.Api.ProductCategory.General);
        
        var success = _productService.Update(new UpdateProductDto
        {
            Id = dto.Id,
            Name = dto.Name,
            Weight = dto.Weight,
            Category = category,
            WarehouseId = dto.WarehouseId ?? 0
        });

        return Task.FromResult(new UpdateProductResponse { Success = success });
    }
    
    public override Task<GetProductResponse> GetById(GetProductByIdRequest request, ServerCallContext context)
    {
        var id = request.Id;
        var dto = _productService.GetById(id);
    
        if (dto == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with id {id} not found"));
        }
    
        var response = new GetProductResponse
        {
            Id = dto.Id,
            Name = dto.Name,
            Weight = dto.Weight,
            Category = (ProductService.Api.ProductCategory)dto.Category,
            CreatedUtc = Timestamp.FromDateTime(dto.CreatedUtc),
            WarehouseId = dto.WarehouseId
        };
    
        return Task.FromResult(response);
    }
    
    public override Task<ListProductResponse> List(Empty request, ServerCallContext context)
    {
        var dtos = _productService.Get();
        var response = new ListProductResponse();

        foreach (var dto in dtos)
        {
            response.Products.Add(new GetProductResponse
            {
                Id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                Category = (ProductService.Api.ProductCategory)dto.Category,
                CreatedUtc = Timestamp.FromDateTime(dto.CreatedUtc),
                WarehouseId = dto.WarehouseId
            });
        }

        return Task.FromResult(response);
    }
}