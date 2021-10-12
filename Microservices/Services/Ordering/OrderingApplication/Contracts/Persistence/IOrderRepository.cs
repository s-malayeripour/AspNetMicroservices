using OrderingDomain.Entities;

namespace OrderingApplication.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
        Task<bool> DeleteOrderByIdAsync(int id);
    }
}
