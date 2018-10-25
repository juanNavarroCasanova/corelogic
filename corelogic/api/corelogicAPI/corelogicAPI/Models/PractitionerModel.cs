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
    public class PractitionerModel
    {

        public PractitionerModel() { }

        [Key]
        public int id { get; set; }
        public String name { get; set; }
        
        public static IList<PractitionerModel> getPractitionersList()
        {
            var file = Path.Combine("Data/practitioners.json");
            var content = System.IO.File.ReadAllText(file);

            // I know the data is a well-formated json so I cast it into a json object to be able to filter it better
            JObject json = JObject.Parse(content);

            IList<JToken> results = json.GetValue("values").Children().ToList();
            IList<PractitionerModel> practitionersList = new List<PractitionerModel>();
            foreach (JToken r in results)
            {
                PractitionerModel practitioner = r.ToObject<PractitionerModel>();
                practitionersList.Add(practitioner);
            }

            practitionersList = practitionersList.OrderBy(p => p.name).ToList();

            return practitionersList;
        }
    }
}
