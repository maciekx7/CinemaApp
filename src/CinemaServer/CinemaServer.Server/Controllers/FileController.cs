using System;
using CinemaServer.Rest.Logic;
using CinemaServer.Rest.Logic.APILogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaServer.Server.Controllers
{
    //[Authorize(Policy = "ValidAccessToken")]
    [Controller]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly ICinemaQueriesHandler cinemaQueriesHandler;
        private readonly ILogger<FileController> _logger;


        public FileController(ILogger<FileController> logger, ICinemaQueriesHandler cinemaQueriesHandler)
        {
            this._logger = logger;
            this.cinemaQueriesHandler = cinemaQueriesHandler;
        }

        [HttpGet]
        [Route("Image/{filename}")]
        public IActionResult GetImage()
        {
            try
            {
                var filename = RouteData.Values["filename"].ToString();
                var result = cinemaQueriesHandler.getFile(filename, EFileType.Image);

                if (result.Item1 == null || result.Item1.Length == 0)
                {
                    _logger.LogInformation($"\" GET /Image/{filename} \" 404");
                    return new StatusCodeResult(404);
                }

                var file = File(result.Item1, result.Item2);
                _logger.LogInformation($"\" GET /Image/{filename} \" 200");

                return file;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                _logger.LogInformation($"\" GET /Image/ \" 400");
                return new StatusCodeResult(400);
            }
        }

        [HttpGet]
        [Route("QR/{filename}")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult GetQr()
        {
            try
            {
                var filename = RouteData.Values["filename"].ToString();
                var result = cinemaQueriesHandler.getFile(filename, EFileType.QR);

                if (result.Item1 == null || result.Item1.Length == 0)
                {
                    _logger.LogInformation($"\" GET /QR/{filename} \" 404");
                    return new StatusCodeResult(404);
                }

                var file = File(result.Item1, result.Item2);
                _logger.LogInformation($"\" GET /QR/{filename} \" 200");

                return file;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                _logger.LogInformation($"\" GET /QR/ \" 400");
                return new StatusCodeResult(400);
            }
        }

        [HttpGet]
        [Route("Occasions/{filename}")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult GetOccasionImmage()
        {
            try
            {
                var filename = RouteData.Values["filename"].ToString();
                var result = cinemaQueriesHandler.getFile(filename, EFileType.Occasion);

                if (result.Item1 == null || result.Item1.Length == 0)
                {
                    _logger.LogInformation($"\" GET /Occasions/{filename} \" 404");
                    return new StatusCodeResult(404);
                }


                var file = File(result.Item1, result.Item2);
                _logger.LogInformation($"\" GET /Occasion/{filename} \" 200");

                return file;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"\" GET /Occasion/ \" 400");
                //Console.WriteLine(e);
                return new StatusCodeResult(400);
            }
        }

    }
}
