using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    public class FormattedTicket
    {
        public int ticketID { get; set; }
        public string key { get; set; }
        public string agentName { get; set; }
        public DateTime startDate { get; set; }
        public string type { get; set; }
        public string priority { get; set; }
        public string company { get; set; }
        public bool completed { get; set; }
        public int totalDuration { get; set; }
        public int summaryStateID { get; set; }
        public int open { get; set; }
        public int inProgress { get; set; }
        public int waiting { get; set; }
        public int internalValidation { get; set; }
    }
}
