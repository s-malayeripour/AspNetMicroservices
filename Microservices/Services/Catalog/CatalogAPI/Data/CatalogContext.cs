using MongoDB.Driver;
using CatalogAPI.Entities;
using CatalogAPI.Data.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CatalogAPI.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration config)
        {
            string connectionString = config.GetValue<string>("DatabaseSettings:ConnectionString");
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(config.GetValue<string>("DatabaseSettings:CollectionName"));
            SeedProducts();
        }

        private void SeedProducts()
        {
            bool anyProductExist = Products.Find(p => true).Any();
            if (!anyProductExist)
            {
                Products.InsertMany(PreconfiguredDatas.GetPreconfiguredProducts());
            }
        }
    }
}
