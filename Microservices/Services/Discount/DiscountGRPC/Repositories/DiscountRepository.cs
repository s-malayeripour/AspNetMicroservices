using Dapper;
using DiscountGRPC.Entities;
using DiscountGRPC.Repositories.Interfaces;
using Npgsql;

namespace DiscountGRPC.Repositories
{
    //I know this class is a total mass class ...
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _config;

        public DiscountRepository(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            //I know this should be seperated from this layer but ...
            using NpgsqlConnection connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();
            Coupon couponResult = await connection.QueryFirstOrDefaultAsync<Coupon>("Select * from Coupons WHERE ProductName=@ProductName", new { ProductName = productName });
            return couponResult ?? new Coupon()
            {
                ProductName = productName,
                Description = "No coupon description !!!",
                Amount = 0
            };
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            int affectedRows = await connection.ExecuteAsync("Insert into Coupons (ProductName, Description, Amount) VALUES (@ProductName,@Description,@Amount)",
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });
            return affectedRows == 0 ? false : true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            int affectedRows = await connection.ExecuteAsync("Update Coupons SET ProductName=@ProductName, Description=@Description, Amount=@Amount",
                new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });
            return affectedRows == 0 ? false : true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            int affectedRows = await connection.ExecuteAsync("Delete FROM Coupons WHERE ProductName=@ProductName", new { ProductName = productName });
            return affectedRows == 0 ? false : true;
        }
    }
}
