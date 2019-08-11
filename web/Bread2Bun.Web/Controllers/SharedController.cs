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
            try
            {
                var result = await countryService.GetCountries();
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return HandleExcpetion(ex);
            }

        }

        [HttpGet, Route("universities")]
        public async Task<IActionResult> GetUniversities()
        {
            try
            {
                var result = await universityService.GetUniversities();
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return HandleExcpetion(ex);
            }

        }
    }
}