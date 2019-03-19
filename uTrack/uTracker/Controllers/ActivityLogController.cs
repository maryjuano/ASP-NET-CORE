using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace uTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        //mock data.
        private List<ActivityLog> logs = new List<ActivityLog>() {
                new ActivityLog() {
                    LogId = "AL-03162019002217",
                    Date = new DateTime(2019, 3, 15).ToString("dd-MM-yyyy"),
                    Project = new Project(){ ClientName = "WVC-SSA_2019", Id = 1, ProjectName = "World Vision Canada", },
                    Billable = 8,
                    NonBillable = 0,
                    Period = Period.Regular
                },
                 new ActivityLog() {
                    LogId = "AL-03162019002144",
                    Date = new DateTime(2019, 3,16).ToString("dd-MM-yyyy"),
                    Project = new Project(){ ClientName = "WVC-SSA_2019", Id = 1, ProjectName = "World Vision Canada", },
                    Billable = 3,
                    NonBillable = 0,
                    Period = Period.Regular
                },
                  new ActivityLog() {
                    LogId = "AL-03162019002156",
                    Date = new DateTime(2019, 3, 16).ToString("dd-MM-yyyy"),
                    Project = new Project(){ ClientName = "WVC-SSA_2019", Id = 1, ProjectName = "World Vision Canada", },
                    Billable = 3,
                    NonBillable = 0,
                    Period = Period.Regular
                },


            };
        // GET: api/ActivityLog
        [HttpGet]
        public IEnumerable<ActivityLog> Get()
        {
            return logs;
        }

        // GET: api/ActivityLog/5
        [HttpGet("{id}", Name = "Get")]
        public ActivityLog Get(string id)
        {
            return logs.Find(p => p.LogId == id);
        }

        // POST: api/ActivityLog
        [HttpPost]
        public void Post([FromBody] ActivityLog value)
        {
            logs.Add(value);
        }

        // PUT: api/ActivityLog/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ActivityLog value)
        {
            RemoveItem(id);
            logs.Add(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            RemoveItem(id);
        }


        public void RemoveItem(string id)
        {
            int index = 0;
            foreach (ActivityLog item in logs)
            {
                if (item.LogId == id)
                {
                    index = logs.IndexOf(item);
                }
            }

            logs.RemoveAt(index);
        }
    }
}
