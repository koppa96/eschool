using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Testing.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectDeletedConsumer : IConsumer<ClassSchoolYearSubjectDeletedEvent>
    {
        private readonly Lazy<TestingContext> lazyDbContext;

        public ClassSchoolYearSubjectDeletedConsumer(Lazy<TestingContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<ClassSchoolYearSubjectDeletedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var classSchoolYearSubject = await dbContext.ClassSchoolYearSubjects
                .SingleOrDefaultAsync(x => x.Class.Id == context.Message.ClassId &&
                                           x.SchoolYear.Id == context.Message.SchoolYearId &&
                                           x.Subject.Id == context.Message.SubjectId);
            if (classSchoolYearSubject != null)
            {
                dbContext.ClassSchoolYearSubjects.Remove(classSchoolYearSubject);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}