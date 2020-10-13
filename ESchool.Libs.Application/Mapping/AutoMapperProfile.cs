using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ESchool.Libs.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface && x.GetInterfaces()
                                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMappable<,>)))
                .ToList();

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IMappable<,>))
                    .Select(x => new
                    {
                        Source = x.GetGenericArguments().First(),
                        Destination = x.GetGenericArguments().Last(),
                        Type = x
                    })
                    .ToList();

                foreach (var @interface in interfaces)
                {
                    var mappingExpression = GetType().GetMethod("CreateMap", new Type[] {})?
                        .MakeGenericMethod(@interface.Source, @interface.Destination)
                        .Invoke(this, null);
                    
                    var mappableInstance = Activator.CreateInstance(type);
                    
                    type.GetMethod("ConfigureMap", new Type[] {})?
                        .Invoke(mappableInstance, new[] {mappingExpression});
                }
            }
        }
    }
}
