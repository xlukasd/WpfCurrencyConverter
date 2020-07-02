using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Shared.CurrencyConversion;

namespace Application.Services.CurrencyConversion
{
    public interface ICurrencyConverter
    {
        Task<Result<OutputObject>> Convert(InputObject inputObject);
    }
}
