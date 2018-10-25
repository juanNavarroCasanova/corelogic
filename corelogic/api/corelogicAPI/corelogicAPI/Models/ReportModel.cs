using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace corelogicAPI.Models
{
    public class ReportModel
    {
        public ReportModel() { }
 
        public ReportModel(int practitioner_id, String practitioner_name, float cost, float revenue, String month) // month will be like such: 20171, 20172, 201810... as in Jan 2017, Feb 2017, Oct 2018...
        {
            this.practitioner_id = practitioner_id;
            this.name = practitioner_name;
            this.cost = cost;
            this.revenue = revenue;
            this.month = month;
        }

        public int practitioner_id { get; set; }
        public String name { get; set; }
        public float cost { get; set; }
        public float revenue { get; set; }
        public String month { get; set; }
    }
}
