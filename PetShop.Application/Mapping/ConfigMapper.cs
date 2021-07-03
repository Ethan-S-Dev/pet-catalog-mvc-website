using AutoMapper;
using PetShop.Application.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Mapping
{
    public class ConfigMapper
    {
        public static Action<IMapperConfigurationExpression> Configuration => cfg=>
        {
            cfg.AddProfile<MapperProfile>();
        };
        
    }
}
