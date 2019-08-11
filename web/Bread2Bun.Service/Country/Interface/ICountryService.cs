using Bread2Bun.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Country.Interface
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryModel>> GetCountries();
    }
}
