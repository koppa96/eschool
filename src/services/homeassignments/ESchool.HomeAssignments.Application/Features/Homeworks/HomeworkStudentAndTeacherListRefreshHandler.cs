using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkStudentAndTeacherListRefreshCommand : IRequest
    {
        public Guid HomeworkId { get; set; }
    }
    
    public class HomeworkStudentAndTeacherListRefreshHandler : IRequestHandler<HomeworkStudentAndTeacherListRefreshCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly LessonService.LessonServiceClient client;

        public HomeworkStudentAndTeacherListRefreshHandler(HomeAssignmentsContext context,
            LessonService.LessonServiceClient client)
        {
            this.context = context;
            this.client = client;
        }
        
        public async Task<Unit> Handle(HomeworkStudentAndTeacherListRefreshCommand request, CancellationToken cancellationToken)
        {
            var homework = await context.Homeworks.Include(x => x.StudentHomeworks)
                .Include(x => x.TeacherHomeworks)
                .SingleAsync(x => x.Id == request.HomeworkId, cancellationToken);

            var response = await client.ListStudentsAndTeachersForLessonAsync(new StudentAndTeacherListRequest
            {
                LessonId = homework.LessonId.ToString()
            }, cancellationToken: cancellationToken);
            
            var studentGuids = response.StudentIds.Select(x => Guid.Parse(x)).ToList();
            var students = await context.Students.Where(x => studentGuids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var teacherGuids = response.TeacherIds.Select(x => Guid.Parse(x)).ToList();
            var teachers = await context.Teachers.Where(x => teacherGuids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            context.StudentHomeworks.RemoveRange(homework.StudentHomeworks
                .Where(x => !studentGuids.Contains(x.StudentId)));
            
            context.TeacherHomeworks.RemoveRange(homework.TeacherHomeworks
                .Where(x => !teacherGuids.Contains(x.TeacherId)));
            
            context.StudentHomeworks.AddRange(students.Where(x => homework.StudentHomeworks.All(sh => sh.Student != x))
                .Select(x => new StudentHomework
                {
                    Homework = homework,
                    Student = x
                }));
            
            context.TeacherHomeworks.AddRange(teachers.Where(x => homework.TeacherHomeworks.All(th => th.Teacher != x))
                .Select(x => new TeacherHomework
                {
                    Homework = homework,
                    Teacher = x
                }));

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}