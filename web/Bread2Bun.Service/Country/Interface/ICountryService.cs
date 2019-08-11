using Bread2Bun.Service.Country.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Country.Interface
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryModel>> GetCountries();
    }
}
