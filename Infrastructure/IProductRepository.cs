using OzonGrpc.Domain;

namespace OzonGrpc.Infrastructure;

public interface IProductRepository
{
    bool Update(Product product);
    ulong Add(Product product);
    Product? GetById(ulong id);
    IEnumerable<Product> Get();
}