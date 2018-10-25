using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using corelogicAPI.Models;

namespace corelogicAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {

        private FilterModel parseQueryString(Microsoft.AspNetCore.Http.IQueryCollection query)
        {
            FilterModel myFilter = new FilterModel();

            if (query.ContainsKey("pid")) // this is expected as an array, like such: ?pid=[1,2,3,5] or pid=9 or pid=1,2,8... etc
            {
                myFilter.practitioner_ids = Array.ConvertAll(query["pid"].ToString().Replace("[", "").Replace("]", "").Split(","), id => int.Parse(id));
            }
            if (query.ContainsKey("startdate"))
            {
                myFilter.startDate = DateTime.Parse(query["startdate"]);
            }
            if (query.ContainsKey("enddate"))
            {
                myFilter.endDate = DateTime.Parse(query["enddate"]);
            }

            return myFilter;
        }

        // GET api/report
        [HttpGet]
        public IEnumerable<ReportModel> Get()
        {
            FilterModel myFilter = new FilterModel();

            // get query strings to generate the filter
            myFilter = parseQueryString(Request.Query);

            IList<AppointmentModel> appointmentsList = AppointmentModel.getAppointmentsList(myFilter);

            Dictionary<int, Dictionary<String, CostRevenueModel>> summatory = new Dictionary<int, Dictionary<String, CostRevenueModel>>();
            foreach (AppointmentModel ap in appointmentsList)
            {
                var keyPractitioner = ap.practitioner_id;
                Dictionary<String, CostRevenueModel> practitionerSlot = null;
                if(!summatory.ContainsKey(keyPractitioner))
                {
                    summatory.Add(keyPractitioner, new Dictionary<string, CostRevenueModel>());
                }
                practitionerSlot = summatory[keyPractitioner];

                var keyMonth = ap.date.Year.ToString() + ap.date.Month.ToString();
                if (practitionerSlot.ContainsKey(keyMonth))
                {
                    practitionerSlot[keyMonth].cost += ap.cost;
                    practitionerSlot[keyMonth].revenue += ap.revenue;
                }
                else
                {
                    practitionerSlot.Add(keyMonth, new CostRevenueModel(ap.cost, ap.revenue));
                }
            }

            IList<ReportModel> ReportResults = new List<ReportModel>();

            int[] practitioner_ids = Request.Query.ContainsKey("pid") ? Array.ConvertAll(Request.Query["pid"].ToString().Replace("[", "").Replace("]", "").Split(","), id => int.Parse(id)) : null;
            IList<PractitionerModel> practitioners = PractitionerModel.getPractitionersList();
            if (practitioner_ids == null || practitioner_ids.Length == 0)
            {
                // if none specified, all of them
                practitioner_ids = practitioners.Select(p => p.id).ToArray();
            } else
            {
                // filter them if there is query string with practitioners ids
                practitioners = practitioners.Where(p => practitioner_ids.Contains(p.id)).ToList();
            }
            foreach (int id in practitioner_ids)
            {
                // TODO: handle non-existent practitioner ids...
                if (summatory.Count > 0)
                {
                    foreach (KeyValuePair<String, CostRevenueModel> sum in summatory[id])
                    {
                        var pname = practitioners.FirstOrDefault(p => p.id == id).name;
                        ReportResults.Add(new ReportModel(id, pname, sum.Value.cost, sum.Value.revenue, sum.Key));
                    }
                }
            }

            Response.ContentType = "application/json";
            return ReportResults;
        }
  
    }
}
