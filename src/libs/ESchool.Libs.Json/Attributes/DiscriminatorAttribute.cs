using System;

namespace ESchool.Libs.Json.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DiscriminatorAttribute : Attribute
    {
        public string DiscriminatorValue { get; }

        public DiscriminatorAttribute(string discriminatorValue)
        {
            DiscriminatorValue = discriminatorValue;
        }
    }
}