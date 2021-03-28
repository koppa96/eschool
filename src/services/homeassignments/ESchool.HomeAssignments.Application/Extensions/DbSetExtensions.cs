using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Extensions
{
    public static class DbSetExtensions
    {
        public static Task<Teacher> GetByUserId(this DbSet<Teacher> teachers, Guid userId, CancellationToken cancellationToken = default)
        {
            return teachers.SingleAsync(x => x.UserId == userId, cancellationToken);
        }
        
        public static Task<Student> GetByUserId(this DbSet<Student> students, Guid userId, CancellationToken cancellationToken = default)
        {
            return students.SingleAsync(x => x.UserId == userId, cancellationToken);
        }

        public static Task<bool> CanViewHomework(this DbSet<Homework> homeworks, Guid userId, Guid homeworkId,
            CancellationToken cancellationToken = default)
        {
            return homeworks.AnyAsync(
                x => x.Id == homeworkId &&
                     (x.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectStudents.Any(
                          s => s.Student.UserId == userId) ||
                      x.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(
                          t => t.Teacher.UserId == userId)
                     ), cancellationToken);
        }
    }
}