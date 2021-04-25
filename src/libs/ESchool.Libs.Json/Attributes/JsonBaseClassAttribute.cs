using System;

namespace ESchool.Libs.Json.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class JsonBaseClassAttribute : Attribute
    {
        public string DiscriminatorPropertyName { get; init; }
    }
}