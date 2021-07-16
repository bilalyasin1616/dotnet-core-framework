using AutoMapper;
using System;
using System.Linq;

namespace Framework.Extensions
{
    public static class TypeExtensions
    {

        public static bool CheckColumnExist(this Type source, string column)
        {
            var sourceProps = source.GetProperties().Where(x => x.CanRead).ToList();
            return sourceProps.Any(x => x.Name.ToLower() == column.ToLower());
        }

        public static D MapEntity<S, D>(this S source)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<S, D>(); });
            var mapper = config.CreateMapper();
            return mapper.Map<S, D>(source);
        }

        public static D MapEntity<S, D>(this S source, D destination)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<S, D>(); });
            var mapper = config.CreateMapper();
            return mapper.Map<S, D>(source, destination);
        }
    }
}
