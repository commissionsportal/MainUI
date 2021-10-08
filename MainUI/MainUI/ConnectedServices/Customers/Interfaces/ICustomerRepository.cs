using MainUI.ConnectedServices.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerListWithCount> GetCustomers(int pageSize, int page);
        Task<IEnumerable<Customer>> GetCustomers(IEnumerable<string> customerIds);
        Task<Customer> GetCustomer(string customerId);
    }
}
