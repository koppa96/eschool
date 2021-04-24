using System;

namespace ESchool.Testing.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TaskTypeAttribute : Attribute
    {
        public Type TaskType { get; }

        public TaskTypeAttribute(Type taskType)
        {
            TaskType = taskType;
        }
    }
}