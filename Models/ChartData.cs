using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pi_store.Models
{
    public class ChartData
    {
        public List<(DateTime, decimal)> RevenueData { get; set; } 
        public Dictionary<string, int> TopSellingProducts { get; set; }
    }
}
