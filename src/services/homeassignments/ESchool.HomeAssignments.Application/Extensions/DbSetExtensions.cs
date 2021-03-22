using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Extensions
{
    public static class DbSetExtensions
    {
        public static Task<bool> IsTeacherAtHomework(this DbSet<Teacher> teachers, Guid userId, Guid homeworkId)
        {
            return teachers.AnyAsync(x =>
                x.UserId == userId && x.TeacherHomeworks.Any(th => th.HomeworkId == homeworkId));
        }

        public static Task<Teacher> GetByUserId(this DbSet<Teacher> teachers, Guid userId, CancellationToken cancellationToken = default)
        {
            return teachers.SingleAsync(x => x.UserId == userId, cancellationToken);
        }
        
        public static Task<Student> GetByUserId(this DbSet<Student> students, Guid userId, CancellationToken cancellationToken = default)
        {
            return students.SingleAsync(x => x.UserId == userId, cancellationToken);
        }
    }
}