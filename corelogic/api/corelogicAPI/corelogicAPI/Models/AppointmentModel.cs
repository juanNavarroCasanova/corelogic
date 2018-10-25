using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System.IO;

namespace corelogicAPI.Models
{
    public class AppointmentModel
    {

        public AppointmentModel() { }

        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        public String client_name { get; set; }
        public String appointment_type { get; set; }
        public float duration { get; set; }
        public float revenue { get; set; }
        public float cost { get; set; }
        public int practitioner_id { get; set; }

        public static IList<AppointmentModel> getAppointmentsList(FilterModel filter)
        {
            var file = Path.Combine("Data/appointments.json");
            var content = System.IO.File.ReadAllText(file);

            // I know the data is a well-formated json so I cast it into a json object to be able to filter it better
            JObject json = JObject.Parse(content);

            IList<JToken> results = json.GetValue("values").Children().ToList();
            IList<AppointmentModel> appointmentsList = new List<AppointmentModel>();
            foreach (JToken r in results)
            {
                AppointmentModel appointment = r.ToObject<AppointmentModel>();
                appointmentsList.Add(appointment);
            }

            if (filter.practitioner_ids != null && filter.practitioner_ids.Length > 0)
            {
                appointmentsList = appointmentsList.Where(a => filter.practitioner_ids.Contains(a.practitioner_id)).ToList<AppointmentModel>();
            }
            if (filter.startDate != null)
            {
                appointmentsList = appointmentsList.Where(a => a.date >= filter.startDate).ToList();
            }
            if (filter.endDate != null)
            {
                appointmentsList = appointmentsList.Where(a => a.date <= filter.endDate).ToList();
            }

            appointmentsList = appointmentsList.OrderBy(a => a.date).ToList();

            return appointmentsList;
        }

        public static AppointmentModel getAppointment(int appointment_id)
        {
            var file = Path.Combine("Data/appointments.json");
            var content = System.IO.File.ReadAllText(file);

            // I know the data is a well-formated json so I cast it into a json object to be able to filter it better
            JObject json = JObject.Parse(content);

            IList<JToken> results = json.GetValue("values").Children().ToList();
            IList<AppointmentModel> appointmentsList = new List<AppointmentModel>();
            foreach (JToken r in results)
            {
                AppointmentModel appointment = r.ToObject<AppointmentModel>();
                appointmentsList.Add(appointment);
            }

            return appointmentsList.FirstOrDefault(a => a.id == appointment_id);
        }
    }
}
