using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Extensions
{
    public static class Mapper
    {
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
            return mapper.Map(source, destination);
        }

        public static IEnumerable<D> MapList<S, D>(this IMapper mapper, IEnumerable<S> objs)
        {
            return objs.Select(s => mapper.Map<D>(s));
        }
    }
}