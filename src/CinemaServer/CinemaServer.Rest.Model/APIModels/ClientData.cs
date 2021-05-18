using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaServer.Rest.Model.APIModels
{
    public class ClientData : ClientBaseData
    {

        [Required]
        [StringLength(45)]
        [EmailAddress]
        public string Email { get; set; }


        public ClientData()
        {

        }

        public ClientData(string name, string lastName, string phone, string email, string password)
        {
            Name = name;
            Lastname = lastName;
            Phone = phone;
            Email = email;
            Password = password;
        }
    }
    
}
