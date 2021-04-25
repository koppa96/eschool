using System;

namespace ESchool.Libs.Json.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class JsonSubClassAttribute : Attribute
    {
        public string Discriminator { get; init; }
        
    }
}