using System.Collections.Concurrent;
using OzonGrpc.Domain;

namespace Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<ulong, Product> _data = new();
    
    public bool Update(Product? product)
    {
        if (product is null)
        {
            return false;
        }
        
        var isExists = _data.ContainsKey(product.Id);
        if (!isExists)
        {
            return false;
        }

        _data[product.Id] = product;
        return true;
    }

    public ulong Add(Product product)
    {
        if (product is null)
        {
            return 0;
        }

        var id = (ulong)_data.Count + 1;
        product.Id = id;
        
        if (!_data.TryAdd(id, product))
        {
            return 0;
        }

        return id;
    }

    public Product? GetById(ulong id)
    {
        if (id == 0)
        {
            return null;
        }

        var isExists = _data.TryGetValue(id, out var product);
        if (!isExists)
        {
            return null;
        }

        return product;
    }

    public ICollection<Product> Get()
    {
        return _data.Values.ToList();
    }
}