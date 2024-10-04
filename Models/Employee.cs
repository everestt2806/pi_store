using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Models
{
    public class Employee
    {
        public string ID { get; set; }             
        public string Name { get; set; }           
        public string Email { get; set; }         
        public string Phone { get; set; }          
        public string Address { get; set; }        
        public decimal Salary { get; set; }        
        public DateTime HireDate { get; set; }     
    }

}
