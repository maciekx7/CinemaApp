using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaServer.Rest.Model.APIModels
{
    public class LoginData
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mail is required")]
        [EmailAddress]
        public string Mail { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Phone]
        [StringLength(12)]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
    }
}
