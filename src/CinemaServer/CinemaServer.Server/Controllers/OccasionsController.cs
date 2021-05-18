using System;
using System.Collections.Generic;
using CinemaServer.Model;
using CinemaServer.Rest.Logic.APILogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaServer.Server.Controllers
{
    [Controller]
    public class OccasionsController
    {
        private readonly ICinemaQueriesHandler cinemaQueriesHandler;
        private readonly ILogger<OccasionsController> _logger;


        public OccasionsController(ILogger<OccasionsController> logger, ICinemaQueriesHandler cinemaQueriesHandler)
        {
            this._logger = logger;
            this.cinemaQueriesHandler = cinemaQueriesHandler;
        }

        [Authorize(Policy = "ValidAccessToken")]
        [HttpGet]
        [Route("Occasions")]
        public IActionResult GetOccasions()
        {

            try
            {
                List<Occasion> occasions = new List<Occasion>();

                occasions = cinemaQueriesHandler.GetOccasions();

                if (occasions == null || occasions.Count == 0)
                {
                    _logger.LogInformation($"\" GET /Occasions/ \" 404");
                    return new StatusCodeResult(404);
                }
                var json = new JsonResult(occasions);
                _logger.LogInformation($"\" GET /Occasions/ \" 200");

                return json;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"\" GET /Occasions/ \" 400");
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }

            
        }
    }
}
