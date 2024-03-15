using OzonGrpc.Domain;

namespace Infrastructure;

public interface IProductRepository
{
    bool Update(Product product);
    ulong Add(Product product);
    Product? GetById(ulong id);
    ICollection<Product> Get();
}