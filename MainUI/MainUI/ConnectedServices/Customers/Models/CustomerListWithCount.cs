namespace MainUI.ConnectedServices.Customers.Models
{
    public class CustomerListWithCount
    {
        public Customer[] Customers { get; set; }
        public int TotalCustomers { get; set; }
    }
}
