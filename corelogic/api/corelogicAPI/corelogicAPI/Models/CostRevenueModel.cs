using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace corelogicAPI.Models
{
    public class CostRevenueModel
    {
        public CostRevenueModel() { }
 
        public CostRevenueModel(float cost, float revenue)
        {
            this.cost = cost;
            this.revenue = revenue;
        }

        public float revenue { get; set; }
        public float cost { get; set; }
    }
}
