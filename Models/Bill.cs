using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Models
{
    using System;

    public class Bill
    {
        public string ID { get; set; }             
        public string OrderID { get; set; }        
        public string ClientID { get; set; }       
        public string EmployeeID { get; set; }     
        public DateTime BillDate { get; set; }     
        public decimal TotalPrice { get; set; }    
    }

}
