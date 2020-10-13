using AutoMapper;

namespace ESchool.Libs.Application.Mapping
{
    public interface IMappable<TSource, TDestination>
    {
        void ConfigureMap(IMappingExpression<TSource, TDestination> mapping) {}
    }
}