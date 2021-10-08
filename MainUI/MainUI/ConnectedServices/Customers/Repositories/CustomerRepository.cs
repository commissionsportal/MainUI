using MainUI.ConnectedServices.Customers.Interfaces;
using MainUI.ConnectedServices.Customers.Models;
using MainUI.ConnectedServices.Customers.Repositories.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IQueryClient _client;

        public CustomerRepository(IQueryClient client)
        {
            _client = client;
        }

        public async Task<Customer> GetCustomer(string customerId)
        {
            var result = await _client.GetCustomer(customerId);
            FixProfileImage(result);
            return result;
        }

        public async Task<CustomerListWithCount> GetCustomers(int pageSize, int page)
        {
            var offset = pageSize * (page - 1);
            var result = await _client.GetCustomers(offset, pageSize);
            result.Customers?.ToList().ForEach(x => FixProfileImage(x));
            return result;
        }

        public async Task<IEnumerable<Customer>> GetCustomers(IEnumerable<string> customerIds)
        {
            var result = await _client.GetCustomers(customerIds);
            result?.ToList().ForEach(x => FixProfileImage(x));
            return result;
        }

        private void FixProfileImage(Customer customer)
        {
            if (customer != null)
            {
                if (string.IsNullOrEmpty(customer.ProfileImage))
                {
                    customer.ProfileImage = "https://cdn1.vectorstock.com/i/thumbs/47/40/default-avatar-photo-placeholder-profile-image-vector-25794740.jpg";
                }
                else
                {
                    customer.ProfileImage = $"https:" + $"//app.commissionsportal.com/demo/faces/{customer.ProfileImage}";
                }
            }
        }
    }
}
