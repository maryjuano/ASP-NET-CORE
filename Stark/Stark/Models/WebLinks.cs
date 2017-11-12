using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stark.Models
{
    public class WebLink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
    }
}
