using MongoDB.Driver;
using CatalogAPI.Entities;

namespace CatalogAPI.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products {  get; }
    }
}
