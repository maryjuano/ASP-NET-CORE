using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class ActivityLog
    {
        public string LogId { get; set; }
        public string Date { get; set; }
        public Project Project { get; set; }       
        public decimal Billable { get; set; }
        public decimal NonBillable { get; set; }
        public Period Period { get; set; }
        public Status Status { get; set; }
    }
}
