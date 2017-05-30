using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Hystrix.Dotnet;

namespace Caps.DotnetMicroservice.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly IHystrixCommandFactory hystrixCommandFactory;

        public ValuesController(IHystrixCommandFactory hystrixCommandFactory)
        {
            this.hystrixCommandFactory = hystrixCommandFactory;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var hystrixCommand = hystrixCommandFactory.GetHystrixCommand("groupKey", "commandKey");
            return hystrixCommand.Execute<IEnumerable<string>>(() => new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

    }
}
