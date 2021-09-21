using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private Rootobject tickets = new Rootobject();
        private List<FormattedTicket> _formattedTickets;
        private static string heads = "ticketID, key, agentName, startDate, type, priority, company, completed, totalDuration, open, inProgress, waiting, internalValidation";

        [HttpGet]
        public String ticketProcess()
        {
            string response;
            try
            {
                string path = @"C:\Users\Gabriel.Aguirre\Desktop\IntegrationTest.json";
                string fileDirectory = Environment.CurrentDirectory + @"\outPutGet.txt";

                StreamReader sr = new StreamReader(path);

                string values = sr.ReadToEnd();

                tickets = JsonConvert.DeserializeObject<Rootobject>(values);

                var fd = formattedData(tickets);

                response = outputData(fd, fileDirectory);

            }
            catch (Exception ex)
            {
                response = ex.Message;
            }          

            return response;
        }

        public List<FormattedTicket> formattedData(Rootobject tickets)
        {
            _formattedTickets = new List<FormattedTicket>();

            foreach (var t in tickets.Tickets)
            {
                FormattedTicket ft = new FormattedTicket();
                ft.ticketID = t.id;
                ft.key = t.key;
                foreach (var f in t.fields)
                {
                    ft.agentName = f.agentName;
                    ft.company = f.company;
                    ft.completed = f.completed;
                    ft.totalDuration = f.totalDuration;
                    ft.startDate = f.startDate;
                    ft.type = f.type;
                    ft.priority = f.priority;

                    foreach (var ss in f.summaryStates)
                    {
                        switch (ss.id)
                        {
                            case 1:
                                ft.open = ss.duration;
                                break;
                            case 2:
                                ft.inProgress = ss.duration;
                                break;
                            case 3:
                                ft.waiting = ss.duration;
                                break;
                            default:
                                ft.internalValidation = ss.duration;
                                break;
                        }

                    }

                }
                _formattedTickets.Add(ft);
            }
            return _formattedTickets;

        }

        public string outputData(List<FormattedTicket> ft, string directory)
        {
            using (StreamWriter outFile = new StreamWriter(directory))
            {
                outFile.WriteLine(heads);
                foreach (var item in _formattedTickets)
                {

                    outFile.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}",
                        item.ticketID,
                        item.key,
                        item.agentName,
                        item.startDate,
                        item.type,
                        item.priority,
                        item.company,
                        item.completed,
                        item.totalDuration,
                        item.summaryStateID,
                        item.open,
                        item.inProgress,
                        item.waiting,
                        item.internalValidation);
                }


            }

            return "Done";
        
        }



        [HttpPost]
        public string ticketpost([FromBody] Rootobject json)
        {
            string response;
            string fileDirectory = Environment.CurrentDirectory + @"\outPutPost.txt";
            try
            {
                var fd = formattedData(json);

                response = outputData(fd, fileDirectory);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            
            }
            //tickets = JsonConvert.DeserializeObject<Rootobject>(json);
            return response;

        }
    }
}
