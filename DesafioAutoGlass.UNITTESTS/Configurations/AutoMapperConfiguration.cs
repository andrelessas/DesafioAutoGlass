using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesafioAutoGlass.APPLICATION.Configuration;

namespace DesafioAutoGlass.UNITTESTS.Configurations
{
    public class AutoMapperConfiguration
    {
        public AutoMapperConfiguration()
        {
            var mapper = new MapperConfiguration(config => config.AddProfile(new AutoMapperConfigurations()));
            Mapper = mapper.CreateMapper();
        }

        public IMapper Mapper { get; set; }
    }
}