using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aclib;

namespace acserver.Controllers
{
    [Route("api/ac")]
    public class AcController
    {
        private AcManager _acManager;
        private Ac _ac;

        public AcController(AcManager acManager)
        {
            _acManager = acManager;
            _ac = acManager.CreateAc("Daikin");
        }

        [HttpPost]
        public string TurnOff(){
            var cmd = new AcCommand().TurnOff();
            _ac.Execute(cmd);
            return "ok";
        }
        [HttpPost]
        public string SetStatus(int temp, int fanPerc, string mode){
            var cmd = new AcCommand()
                .SetTemperature(temp)
                .SetFanSpeed(_ac, (byte)fanPerc);
            if(!string.IsNullOrEmpty(mode)){
                if(mode=="cold") cmd.Cold();
                if(mode=="heat") cmd.Heat();
            }
            _ac.Execute(cmd);
            return "ok";
        }
    }
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
