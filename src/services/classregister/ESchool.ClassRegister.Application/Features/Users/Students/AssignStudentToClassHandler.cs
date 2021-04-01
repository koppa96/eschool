using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects;
using ESchool.Libs.Domain.Extensions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users.Students
{
    public class AssignStudentToClassCommand : IRequest
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }
    
    public class AssignStudentToClassHandler : IRequestHandler<AssignStudentToClassCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IPublishEndpoint publishEndpoint;

        public AssignStudentToClassHandler(ClassRegisterContext context, IPublishEndpoint publishEndpoint)
        {
            this.context = context;
            this.publishEndpoint = publishEndpoint;
        }
        
        public async Task<Unit> Handle(AssignStudentToClassCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Students.FindOrThrowAsync(request.StudentId, cancellationToken);
            if (student.ClassId != null)
            {
                throw new InvalidOperationException("Ez a diák már hozzá van rendelve egy osztályhoz.");
            }

            var @class = await context.Classes.Include(x => x.ClassSchoolYears)
                .ThenInclude(x => x.ClassSchoolYearSubjects)
                .SingleAsync(x => x.Id == request.ClassId, cancellationToken);
            
            student.Class = @class;
            await context.SaveChangesAsync(cancellationToken);
            
            foreach (var classSchoolYearSubject in @class.ClassSchoolYears.SelectMany(x => x.ClassSchoolYearSubjects))
            {
                await publishEndpoint.Publish(new ClassSchoolYearSubjectCreatedOrUpdatedEvent
                {
                    Id = classSchoolYearSubject.Id
                }, CancellationToken.None);
            }
            
            return Unit.Value;
        }
    }
}