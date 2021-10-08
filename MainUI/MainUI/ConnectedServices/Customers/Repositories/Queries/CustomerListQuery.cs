using MainUI.ConnectedServices.Customers.Models;
using System.Threading.Tasks;

namespace MainUI.ConnectedServices.Customers.Repositories.Queries
{
    public static class CustomerListQuery
    {
        public static async Task<CustomerListWithCount> GetCustomers(this IQueryClient client, int offset, int pageSize)
        {
            var result = await client.PostQuery<CustomerListWithCount>(
@"{
              customers(offset: " + offset + ", first: " + pageSize + @") {
                id
                companyName
                fullName
                enrollDate
                profileImage
                status
                customerType
                emailAddress
                phoneNumbers {
                  type
                  number
                }
              },
              totalCustomers
            }");

            return result;
        }
    }
}
