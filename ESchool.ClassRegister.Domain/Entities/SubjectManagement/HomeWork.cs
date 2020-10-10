using System;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class HomeWork
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
