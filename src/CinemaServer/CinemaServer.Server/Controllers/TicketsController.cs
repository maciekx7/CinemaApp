using System;
using CinemaServer.Rest.Logic.APILogic;
using CinemaServer.Rest.Model.APIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaServer.Server.Controllers
{

    [Controller]
    [Route("[controller]")] 
    public class TicketsController : ControllerBase
    {
        private readonly ICinemaQueriesHandler cinemaQueriesHandler;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ILogger<TicketsController> logger, ICinemaQueriesHandler cinemaQueriesHandler)
        {
            this._logger = logger;
            this.cinemaQueriesHandler = cinemaQueriesHandler;

        }

        [HttpGet]
        [Route("Types")]
        public IActionResult getTicketTypes()
        {
            try
            {
                var result = cinemaQueriesHandler.GetReservationTypes();
                if(result == null || result.Count <=0)
                {
                    _logger.LogInformation($"\" GET /Tickets/Types \" 404");
                    return new StatusCodeResult(404);
                }

                var types = new JsonResult(result);
                _logger.LogInformation($"\" GET /Tickets/Types \" 200");

                return types;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"\" GET /Tickets/Types \" 400");
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }
        }

        [HttpGet]
        [Route("MyTickets/{email}")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult GetTickets()
        {
            try
            {
                var email = RouteData.Values["email"].ToString();

                var tickets = new JsonResult(cinemaQueriesHandler.GetTickets(email));
                _logger.LogInformation($"\" GET /Tickets/MyTickets?{email} \" 200"); 
                return tickets;
            } catch(Exception e)
            {

                Console.WriteLine(e);
                _logger.LogInformation($"\" GET /Tickets/MyTickets? \" 400");
                return new StatusCodeResult(400);
            }
            
        }

        
        [HttpPost]
        [Route("Buy")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult BuyTicket([FromBody] BuyTicketData ticketData)
        {
            try
            {
                var email = Request.Headers["email"];
                var result = cinemaQueriesHandler.BuyTicket(ticketData, email);
                if (result == -1)
                {
                    return new StatusCodeResult(400);

                }
                if(result == 0)
                {
                    return new StatusCodeResult(404);
                }
                return new StatusCodeResult(201);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }
        }


        
        [HttpGet]
        [Route("filtered/{sql}")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult FilteredTickets()
        {
            try
            {
                string sql = RouteData.Values["sql"].ToString();
                var result = cinemaQueriesHandler.GetFilteredTickets(sql);

                if(result == null || result.Count == 0)
                {
                    _logger.LogInformation($"\" GET /Tickets/filtered?{sql} \" 404");
                    return new StatusCodeResult(404);
                }

                var json = new JsonResult(result);
                _logger.LogInformation($"\" GET /Tickets/filtered?{sql} \" 200");

                return json;
            } catch(Exception e)
            {
                _logger.LogInformation($"\" GET /Tickets/filtered? \" 400");
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }
           
        }
        
    }
}
