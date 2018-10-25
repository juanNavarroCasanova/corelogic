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
    public class PractitionersController : Controller
    {
        // GET api/practitioners
        [HttpGet]
        public IEnumerable<PractitionerModel> Get()
        {
            Response.ContentType = "application/json";
            return PractitionerModel.getPractitionersList();
        }

        // GET api/practitioners/5
        [HttpGet("{id}")]
        public PractitionerModel Get(int id)
        {
            PractitionerModel prac = PractitionerModel.getPractitionersList().FirstOrDefault(p => p.id == id);
            return prac;
        }

  
    }
}
