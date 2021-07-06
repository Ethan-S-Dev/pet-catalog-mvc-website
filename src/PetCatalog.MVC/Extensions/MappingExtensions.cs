using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Extensions
{
    public static class MappingExtensions
    {
        public static IEnumerable<TResult> MapList<TResult,T>(this IMapper mapper, IEnumerable<T> lsit)
        {
            return mapper.Map<IEnumerable<TResult>>(lsit);
        }
    }
}
