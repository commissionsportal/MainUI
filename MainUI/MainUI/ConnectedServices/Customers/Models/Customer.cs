using System;
using System.Linq;

namespace MainUI.ConnectedServices.Customers.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime? EnrollDate { get; set; }
        public string ProfileImage { get; set; }
        public string Status { get; set; }
        public string CustomerType { get; set; }
        public string EmailAddress { get; set; }
        public PhoneNumber[] PhoneNumbers { get; set; }

        public string MainPhone
        {
            get
            {
                var cellPhone = PhoneNumbers?.Where(x => x.Type == "CellPhone").FirstOrDefault();
                return cellPhone?.Number ?? string.Empty;
            }
        }

        public string CustomerTypeText
        {
            get
            {
                if (CustomerType == "1") return "Team Member";
                return "Preferred Customer";
            }
        }

        public string StatusColor
        {
            get
            {
                if (Status == "Active") return "green";
                if (Status == "Inactive") return "red";

                return "yellow";
            }
        }
    }
}
