using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using corelogicAPI.Models;
using System.Net.Http;

namespace corelogicAPI.Controllers { 

    [Route("api/[controller]")]
    public class AppointmentsController : Controller
    {

        private FilterModel parseQueryString (Microsoft.AspNetCore.Http.IQueryCollection query)
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

        // GET api/appointments
        [HttpGet]
        public IEnumerable<AppointmentModel> Get()
        {
            Response.ContentType = "application/json";

            // check if there is query string content so we filter the returned json by those values
            FilterModel myFilter = parseQueryString(Request.Query);

            // get the list of appointments, passing the filter
            IList<AppointmentModel> appointmentsList = AppointmentModel.getAppointmentsList(myFilter);

            return appointmentsList;
        }

        // GET api/appointments/5
        [HttpGet("{id}")]
        public AppointmentModel Get(int id)
        {
            Response.ContentType = "application/json";

            // get the list of appointments, passing the filter
            return AppointmentModel.getAppointment(id);
        }


    }
}
