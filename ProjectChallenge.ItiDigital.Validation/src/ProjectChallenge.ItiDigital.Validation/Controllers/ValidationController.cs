using Microsoft.AspNetCore.Mvc;
using ProjectChallenge.ItiDigital.Validation.Entities;
using ProjectChallenge.ItiDigital.Validation.Interfaces.Services;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace ProjectChallenge.ItiDigital.Validation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidationController : ControllerBase
    {
        private readonly IValidationService validationService;
        public ValidationController(IValidationService validationService)
        {
            this.validationService = validationService;
        }

        [HttpPost("Password")]
        [SwaggerOperation(Summary = "Returns whether a password is valid.", Description = "")]
        [SwaggerResponse(200, "Password is valid.", typeof(PasswordResponse))]
        [SwaggerResponse(400, "Password is not valid.", typeof(PasswordResponse))]
        [SwaggerResponse(403, "Password is not valid.", typeof(string))]
        [SwaggerResponse(500, "An erro in proccess.", typeof(string))]
        public IActionResult PasswordValidation(PasswordRequest passwordRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(passwordRequest?.Password))
                {
                    Log.Warning("Password cannot empy or null.");
                    return StatusCode(403, "Password cannot empy or null.");
                }

                Log.Information("Send to service Validation.");
                var result = validationService.ValidationPassaword(passwordRequest.Password);
                if (result.IsValid)
                {
                    Log.Information("Minimum password requirement has been met.");
                    return StatusCode(200, result);
                }

                Log.Information("Minimum password requirement has not been met.");
                return StatusCode(400, result);
            }
            catch(Exception ex)
            {
                Log.Error($"An erro in proccess. Error is {ex}");
                return StatusCode(500, "It was not possible to validate the password.");
            }
        }
    }
}
