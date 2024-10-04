﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Models
{
    public class Product
    {
        public string ID { get; set; }            
        public string Name { get; set; }         
        public string Description { get; set; }    
        public decimal Price { get; set; }       
        public int Quantity { get; set; }       
    }

}
