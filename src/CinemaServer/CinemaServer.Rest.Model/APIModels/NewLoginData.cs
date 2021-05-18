using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaServer.Rest.Model.APIModels
{
    public class NewLoginData
    {
        [Required(ErrorMessage = "Mail is required")]
        [EmailAddress]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
