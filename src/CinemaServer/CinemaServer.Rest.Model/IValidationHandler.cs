using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaServer.Rest.Model
{
    public interface IValidationHandler
    {
        bool ValidateUniqueEmail(string email);

       
    }
}
