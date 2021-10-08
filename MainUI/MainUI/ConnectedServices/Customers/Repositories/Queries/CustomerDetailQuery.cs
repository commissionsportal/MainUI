using MainUI.ConnectedServices.Customers.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Customers.Repositories.Queries
{
    public static class CustomerDetailQuery
    {
        public static async Task<Customer> GetCustomer(this IQueryClient client, string customerId)
        {
            var result = await client.PostQuery<GetCustomerDetailResult>(
                    @"{customers (idList: [""" + customerId + @"""]) {
                        id
                        companyName
                        fullName
                        enrollDate
                        profileImage
                        status
                    }}");

            return result.Customers.FirstOrDefault();
        }
    }

    internal class GetCustomerDetailResult
    {
        public Customer[] Customers { get; set; }
    }
}
