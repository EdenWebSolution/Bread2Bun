﻿using AutoMapper;
using Bread2Bun.Data;
using Bread2Bun.Service.University.Interface;
using Bread2Bun.Service.University.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bread2Bun.Service.University
{
    public class UniversityService : IUniversityService
    {
        private readonly Bread2BunContext bread2BunContext;
        private readonly IMapper mapper;

        public UniversityService(Bread2BunContext bread2BunContext, IMapper mapper)
        {
            this.bread2BunContext = bread2BunContext;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<UniversityModel>> GetUniversities()
        {
            var entity = await bread2BunContext.University.ToListAsync();
            var universities = mapper.Map<IEnumerable<UniversityModel>>(entity);
            return universities;
        }
    }
}
