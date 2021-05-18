using System;
using System.ComponentModel.DataAnnotations;
using CinemaServer.Rest.Model.Validators;

namespace CinemaServer.Rest.Model.APIModels
{
    public class ClientRegisterData : ClientBaseData
    {

        [Required]
        [StringLength(45)]
        [EmailAddress]
        [UniqueClientEmailRegister]  //Tu jest element unikalności maila
        public string Email { get; set; }


        public ClientRegisterData(string name, string lastName, string phone, string email, string password)
        {
            Name = name;
            Lastname = lastName;
            Phone = phone;
            Email = email;
            Password = password;
        }

        public ClientRegisterData() {  }
    }
    
}
