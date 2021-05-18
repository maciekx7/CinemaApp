using System;
using System.ComponentModel.DataAnnotations;
using CinemaServer.Rest.Model.APIModels;

namespace CinemaServer.Rest.Model.Validators
{
    public class UniqueClientEmailRegister : ValidationAttribute
    {
        public UniqueClientEmailRegister()
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var client = (ClientRegisterData)validationContext.ObjectInstance;

            var service = (IValidationHandler)validationContext
                         .GetService(typeof(IValidationHandler));

            if (service.ValidateUniqueEmail(client.Email))
            {
                return new ValidationResult("Email is already used");
            }
            return ValidationResult.Success;
        }

        }
    }
