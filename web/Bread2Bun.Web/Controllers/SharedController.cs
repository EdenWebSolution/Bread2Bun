using System.Collections.Generic;
using System.Threading.Tasks;
using Bread2Bun.Common.Model;
using Bread2Bun.Service.Country.Interface;
using Bread2Bun.Service.Security.Interface;
using Bread2Bun.Service.University.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bread2Bun.Web.Controllers
{
    [Route("api/shared")]
    public class SharedController : BaseAPIController
    {
        private readonly ICountryService countryService;
        private readonly IUniversityService universityService;
        private readonly IConfiguration configuration;
        private readonly ISecurityService securityService;

        public SharedController(
            ICountryService countryService,
            IUniversityService universityService,
            IConfiguration configuration,
            ISecurityService securityService
            )
        {
            this.countryService = countryService;
            this.universityService = universityService;
            this.configuration = configuration;
            this.securityService = securityService;
        }

        [HttpGet("countries"), AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CountryModel>), 200)]
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

        [HttpGet("universities"), AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<UniversityModel>), 200)]
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

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsersSummary([FromQuery]string searchTerm)
        {
            try
            {
                var result = await securityService.GetAllUsers(searchTerm);
                return Ok(result);

            }
            catch (System.Exception ex)
            {

                return HandleExcpetion(ex);
            }
        }
    }
}