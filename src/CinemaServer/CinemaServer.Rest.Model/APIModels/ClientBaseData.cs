using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaServer.Rest.Model.APIModels
{
    public class ClientBaseData
    {
        [Required]
        [StringLength(45, ErrorMessage = "{0} length can't be more than {1}.")]
        public string Name { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "Last Name length can't be more than {1}.")]
        public string Lastname { get; set; }

        [Required]
        [Phone]
        [StringLength(12)] //FIXME Powinnismy ograniczyc liczbę znakow w bazie numeru telefonu
        public string Phone { get; set; }


        [Required]
        public string Password { get; set; }

        public ClientBaseData()
        {

        }

        public ClientBaseData(string name, string lastName, string phone, string password)
        {
            Name = name;
            Lastname = lastName;
            Phone = phone;
            Password = password;
        }
    }
}
