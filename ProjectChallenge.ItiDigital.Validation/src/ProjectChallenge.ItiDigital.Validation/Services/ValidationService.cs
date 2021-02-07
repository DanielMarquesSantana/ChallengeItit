using ProjectChallenge.ItiDigital.Validation.Entities;
using ProjectChallenge.ItiDigital.Validation.Interfaces.Services;
using Serilog;
using System;

namespace ProjectChallenge.ItiDigital.Validation.Services
{
    public class ValidationService : IValidationService
    {

        public PasswordResponse ValidationPassaword(string password)
        {
            try
            {
                Log.Information("Calling proccess to validate password.");
                var isValid = password.PatternPassword();

                Log.Information("Create response data to request.");
                var response = new PasswordResponse
                {
                    IsValid = isValid
                };

                return response;
            }
            catch(Exception ex)
            {
                Log.Error($"An error in proccess validating password. Error is: {ex}");
                return new PasswordResponse
                {
                    IsValid = false
                };
            }
        }
    }
}
