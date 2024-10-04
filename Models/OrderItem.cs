using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Models
{
    public class OrderItem
    {
        public string ID { get; set; }           
        public string OrderID { get; set; }        
        public string ProductID { get; set; }      
        public int Quantity { get; set; }          
    }

}
