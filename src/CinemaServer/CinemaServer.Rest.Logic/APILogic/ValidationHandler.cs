using CinemaServer.Logic;
using CinemaServer.Rest.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaServer.Rest.Logic.APILogic
{
    public class ValidationHandler : IValidationHandler
    {
        private readonly ICinemaRepository cinemaRepository;
        public ValidationHandler(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public bool ValidateUniqueEmail(string email)
        {
            return cinemaRepository.isEmailUsed(email);
        }
    }
}
