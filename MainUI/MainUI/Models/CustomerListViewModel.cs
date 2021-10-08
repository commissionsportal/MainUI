using MainUI.ConnectedServices.Customers.Models;
using System;

namespace MainUI.Models
{
    public class CustomerListViewModel
    {
        public Customer[] Customers { get; set; }
        
        public int PageSize { get; set; }
        public int PrevPage { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int LastPage { get; set; }
        public int[] DisplayPages { get; set; }
        public int TotalCustomers { get; set; }

        public string Active(int pageId)
        {
            return pageId == CurrentPage ? "active" : "";
        }

        public int ShowingFrom { get { return ((CurrentPage - 1) * PageSize) + 1; } }
        public int ShowingTo { get { return ShowingFrom - 1 + Math.Min(Customers.Length, PageSize); } }

        public string PrevDisabled { get { return CurrentPage == 1 ? "disabled" : ""; } }
        public string NextDisabled { get { return CurrentPage == LastPage ? "disabled" : ""; } }
    }
}
