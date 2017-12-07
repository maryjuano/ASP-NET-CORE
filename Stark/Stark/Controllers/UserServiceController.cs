using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stark.DAL;

namespace Stark.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserServiceController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UserServiceController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: api/UserService
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserService/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/UserService
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/UserService/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
