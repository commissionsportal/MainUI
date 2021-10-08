using MainUI.ConnectedServices.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Customers.Repositories.Queries
{
    public static class CustomerDetailsQuery
    {
        public static async Task<IEnumerable<Customer>> GetCustomers(this IQueryClient client, IEnumerable<string> ids)
        {
            string idList = string.Join("\",\"", ids);

            var result = await client.PostQuery<GetCustomersResult>(@"
            {
                customers (idList: [""" + idList + @"""]) 
                {
                    id
                    fullName
                    enrollDate
                    profileImage,
                    status
                    customerType
                }
            }");

            return result.Customers;
        }
    }

    public class GetCustomersResult
    {
        public Customer[] Customers { get; set; }
    }
}
