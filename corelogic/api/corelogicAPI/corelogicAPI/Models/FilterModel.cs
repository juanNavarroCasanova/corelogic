using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace corelogicAPI.Models
{
    public class FilterModel
    {
        public FilterModel() { }
 
        public FilterModel(int[] practitioner_ids, Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            this.practitioner_ids = practitioner_ids;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public int[] practitioner_ids { get; set; }
        public Nullable<DateTime> startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
    }
}
