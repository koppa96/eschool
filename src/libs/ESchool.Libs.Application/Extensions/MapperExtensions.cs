using AutoMapper;

namespace ESchool.Libs.Application.Extensions
{
    public static class MapperExtensions
    {
        public static bool TryMap<TDestination>(this IMapper mapper, object source, out TDestination result)
        {
            try
            {
                result = mapper.Map<TDestination>(source);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}