using System.Threading.Tasks;
using Bread2Bun.Service.Country.Interface;
using Bread2Bun.Service.University.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/shared")]
    public class SharedController : BaseAPIController
    {
        private readonly ICountryService countryService;
        private readonly IUniversityService universityService;

        public SharedController(ICountryService countryService, IUniversityService universityService)
        {
            this.countryService = countryService;
            this.universityService = universityService;
        }

        [HttpGet, Route("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await countryService.GetCountries();
            return Ok(result);
        }

        [HttpGet, Route("universities")]
        public async Task<IActionResult> GetUniversities()
        {
            var result = await universityService.GetUniversities();
            return Ok(result);
        }
    }
}