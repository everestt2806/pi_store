using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Models
{
    public class Order
    {
        public string ID { get; set; }             
        public string ClientID { get; set; }       
        public string EmployeeID { get; set; }     
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
    }
}
