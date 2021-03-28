using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectCreatedOrUpdatedConsumer : IConsumer<ClassSchoolYearSubjectCreatedOrUpdatedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;
        private readonly ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceClient client;

        public ClassSchoolYearSubjectCreatedOrUpdatedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext,
            ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceClient client)
        {
            this.lazyDbContext = lazyDbContext;
            this.client = client;
        }

        public async Task Consume(ConsumeContext<ClassSchoolYearSubjectCreatedOrUpdatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var classSchoolYearSubjectDetails = await client.GetDetailsAsync(new ClassSchoolYearSubjectDetailsRequest
            {
                Id = context.Message.Id.ToString()
            });

            var classSchoolYearSubject = await dbContext.ClassSchoolYearSubjects
                .Include(x => x.ClassSchoolYearSubjectStudents)
                .Include(x => x.ClassSchoolYearSubjectTeachers)
                .SingleOrDefaultAsync(x => x.Id == context.Message.Id);

            if (classSchoolYearSubject == null)
            {
                classSchoolYearSubject = new ClassSchoolYearSubject
                {
                    Id = context.Message.Id,
                    ClassId = Guid.Parse(classSchoolYearSubjectDetails.ClassId),
                    SubjectId = Guid.Parse(classSchoolYearSubjectDetails.SubjectId),
                    SchoolYearId = Guid.Parse(classSchoolYearSubjectDetails.SchoolYearId)
                };
                dbContext.ClassSchoolYearSubjects.Add(classSchoolYearSubject);
            }

            dbContext.ClassSchoolYearSubjectTeachers.RemoveRange(classSchoolYearSubject.ClassSchoolYearSubjectTeachers);
            dbContext.ClassSchoolYearSubjectTeachers.AddRange(classSchoolYearSubjectDetails.TeacherIds
                .Select(x => new ClassSchoolYearSubjectTeacher()
                {
                    ClassSchoolYearSubjectId = classSchoolYearSubject.Id,
                    TeacherId = Guid.Parse(x)
                }));

            dbContext.ClassSchoolYearSubjectStudents.RemoveRange(classSchoolYearSubject.ClassSchoolYearSubjectStudents);
            dbContext.ClassSchoolYearSubjectStudents.AddRange(classSchoolYearSubjectDetails.StudentIds
                .Select(x => new ClassSchoolYearSubjectStudent
                {
                    ClassSchoolYearSubjectId = classSchoolYearSubject.Id,
                    StudentId = Guid.Parse(x)
                }));

            await dbContext.SaveChangesAsync();
        }
    }
}