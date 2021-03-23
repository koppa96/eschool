using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using MassTransit;

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

            var classSchoolYearSubject = await dbContext.ClassSchoolYearSubjects.FindAsync(context.Message.Id);
            if (classSchoolYearSubject == null)
            {
                classSchoolYearSubject = new ClassSchoolYearSubject
                {
                    Id = context.Message.Id,
                    ClassId = Guid.Parse(classSchoolYearSubjectDetails.ClassId),
                    SubjectId = Guid.Parse(classSchoolYearSubjectDetails.SubjectId),
                    SchoolYearId = Guid.Parse(classSchoolYearSubjectDetails.SchoolYearId)
                }
            }
        }
    }
}