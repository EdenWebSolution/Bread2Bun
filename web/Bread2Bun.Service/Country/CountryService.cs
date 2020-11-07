using AutoMapper;
using Bread2Bun.Common.Model;
using Bread2Bun.Data;
using Bread2Bun.Service.Country.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.Country
{
    public class CountryService : ICountryService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;

        public CountryService(Bread2BunContext bread2BunContext, IMapper mapper)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CountryModel>> GetCountries()
        {
            var asasd = bread2BunContext.Country as IQueryable;
            var entity = await bread2BunContext.Country.OrderBy(o => o.Name).AsNoTracking().ToListAsync();
            var countries = mapper.Map<IEnumerable<CountryModel>>(entity);
            return countries;
        }
    }
}
