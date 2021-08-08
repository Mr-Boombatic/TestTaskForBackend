using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System;
using System.Threading;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LottoController : ControllerBase
    {

        private readonly ILogger<LottoController> _logger;
        private LottoContext db = new LottoContext();

        public LottoController(ILogger<LottoController> logger)
        {
            _logger = logger;
        }

        [Route("GetTickets")]
        [HttpGet]
        public async Task<ActionResult<string>> GetTickets()
        {
            db.Tickets.Load();
            string json = JsonSerializer.Serialize<List<Ticket>>(db.Tickets.ToList());
            return json;
        }

        [Route("GetCirculations")]
        [HttpGet]
        public async Task<ActionResult<string>> GetCirculations()
        {
            db.Circulations.Load();
            string json = JsonSerializer.Serialize<List<Circulation>>(db.Circulations.ToList());
            return json;
        }

        [Route("RegistrTicket")]
        [HttpPost]
        public void RegistrTickets()
        {
            var jsonTicket = Encoding.Default.GetString(Request.BodyReader.ReadAsync().Result.Buffer.FirstSpan);
            var ticket = JsonSerializer.Deserialize<Ticket>(jsonTicket);
            try
            {
                db.Tickets.Load();
                db.Circulations.Load();

                Circulation circulation = null;
                if (db.Circulations.Where(c => c.Circulation1 == ticket.СirculationNum).Count() != 0)
                    circulation = db.Circulations.Where(c => c.Circulation1 == ticket.СirculationNum).First();

                if (circulation != null)
                {
                    ticket.СirculationNumNavigation = circulation;
                    circulation.Tickets.Add(ticket);
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }
                else
                {
                    // генерация выйгрышной позиции, если отсутствует тираж
                    var winnerNums = new List<int>();
                    Random rnd = new Random();
                    for (int i = 0; i < 6; i++)
                        winnerNums.Add(rnd.Next(1, 36));

                    circulation = new Circulation() { Circulation1 = ticket.СirculationNum, WinnerPosition = string.Join(";", winnerNums) + ";", Tickets = new List<Ticket>() };
                    circulation.Tickets.Add(ticket);
                    db.Circulations.Add(circulation);
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            { }
        }

        [Route("ChangeTicket")]
        [HttpPost]
        public void ChangeTicket()
        {
            var jsonTicket = Encoding.Default.GetString(Request.BodyReader.ReadAsync().Result.Buffer.FirstSpan);
            var ticket = JsonSerializer.Deserialize<Ticket>(jsonTicket);
            try
            {
                db.Tickets.Load();
                var replaceableTicket = db.Tickets.Where(t => t.Variant == ticket.Variant).First();
                replaceableTicket.СirculationNum = ticket.СirculationNum;
                replaceableTicket.SelectedNum = ticket.SelectedNum;


                Circulation circulation = null;
                if (db.Circulations.Where(c => c.Circulation1 == ticket.СirculationNum).Count() != 0)
                {
                    circulation = db.Circulations.Where(c => c.Circulation1 == ticket.СirculationNum).First();
                    replaceableTicket.СirculationNumNavigation = circulation;
                    db.Circulations.Add(circulation);
                }
                else
                {
                    var winnerNums = new List<int>();
                    Random rnd = new Random();
                    for (int i = 0; i < 6; i++)
                        winnerNums.Add(rnd.Next(1, 36));

                    circulation = new Circulation() { 
                        WinnerPosition = string.Join(";", winnerNums) + ";" , 
                        Circulation1 = replaceableTicket.СirculationNum, 
                        Tickets = new List<Ticket> ()
                        };
                    circulation.Tickets.Add(replaceableTicket);
                    replaceableTicket.СirculationNumNavigation = circulation;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            { }
        }

        [Route("DeleteTicket")]
        [HttpPost]
        public void DeleteTicket()
        {
            var jsonNumTicket = Encoding.Default.GetString(Request.BodyReader.ReadAsync().Result.Buffer.FirstSpan);
            var numTicket = JsonSerializer.Deserialize<string>(jsonNumTicket);
            try
            {
                db.Tickets.Load();
                var changeableTicket = db.Tickets.Where(t => t.Variant == int.Parse(numTicket)).First();
                db.Tickets.Remove(changeableTicket);
                db.SaveChanges();
            }
            catch (Exception ex)
            { }
        }

        [Route("FilterTickets")]
        [HttpPost]
        public async Task<ActionResult<string>> FilterTickets()
        {
            var jsonNumTicket = Encoding.Default.GetString(Request.BodyReader.ReadAsync().Result.Buffer.FirstSpan);
            var numbersTicket = JsonSerializer.Deserialize<string>(jsonNumTicket);

            Microsoft.Data.SqlClient.SqlParameter param = new Microsoft.Data.SqlClient.SqlParameter("@template", numbersTicket == ";" ? "" : numbersTicket);
            var tickets = db.Tickets.FromSqlRaw("FilterTickets @template", param).ToList();
            jsonNumTicket = JsonSerializer.Serialize<List<Ticket>>(tickets);
            return jsonNumTicket;
        }
    }
}
