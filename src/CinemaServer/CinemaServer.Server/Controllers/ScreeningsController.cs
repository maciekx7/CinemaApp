using System;
using CinemaServer.Rest.Logic.APILogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CinemaServer.Rest.Model.APIModels.ScreeningsAPIClasses;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CinemaServer.Server.Controllers
{
    // [Authorize(Policy = "ValidAccessToken")]
    [Controller]
    [Route("[controller]")]
    public class ScreeningsController : Controller
    {
        private readonly ILogger<ScreeningsController> _logger;
        private readonly ICinemaQueriesHandler cinemaQueriesHandler;
        public ScreeningsController(ILogger<ScreeningsController> logger, ICinemaQueriesHandler cinemaQueriesHandler)
        {
            this.cinemaQueriesHandler = cinemaQueriesHandler;
            this._logger = logger;
        }

        [HttpGet]
        [Route("/Screenings/Get")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult GetScreeningsAtDay([FromQuery] string date)
        {
            try
            {
                var result = cinemaQueriesHandler.GetScreenings(date);
                if(result == null || result.Count == 0)
                {
                    _logger.LogInformation($"\" GET /Screenings/Get?date={date} \" 404");
                    return new StatusCodeResult(404);
                }


                var json = new JsonResult(result);
                _logger.LogInformation($"\" GET /Screenings/Get?date={date} \" 200");

                return json;
            } catch(Exception e)
            {
                _logger.LogInformation($"\" GET /Screenings/Get? \" 400");
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }
        }

        [HttpGet]
        [Route("/Screanings/SeatsAvaliable")]
        [Authorize(Policy = "ValidAccessToken")]
        [ValidateModel]
        public IActionResult GetSeatsAvaliable(ScreeninigSingleWithoutMovieBodyData screening)
        {
            try
            {
                var seats = cinemaQueriesHandler.GetAvaliableSeats(screening);
                
                if (seats == null || seats.Count == 0)
                {
                    _logger.LogInformation($"\" GET /Screanings/SeatsAvaliable?MovieId={screening.MovieId}&Time={screening.Time}&Date={screening.Date} \" 404");

                    return new StatusCodeResult(404);
                }

                var seatsList = new JsonResult(seats);
                _logger.LogInformation($"\" GET /Screanings/SeatsAvaliable?MovieId={screening.MovieId}&Time={screening.Time}&Date={screening.Date} \" 200");

                return seatsList;
            } catch(Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation($"\" GET /Screanings/SeatsAvaliable? \" 400");

                return new StatusCodeResult(400);
            }

        }


        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
        }

    }
}
