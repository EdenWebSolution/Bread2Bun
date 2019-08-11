using Bread2Bun.Service.University.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.University.Interface
{
    public interface IUniversityService
    {
        Task<IEnumerable<UniversityModel>> GetUniversities();
    }
}
