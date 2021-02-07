using ProjectChallenge.ItiDigital.Validation.Entities;
using System.Threading.Tasks;

namespace ProjectChallenge.ItiDigital.Validation.Interfaces.Services
{
    public interface IValidationService
    {
        PasswordResponse ValidationPassaword(string passwordRequest);
    }
}
