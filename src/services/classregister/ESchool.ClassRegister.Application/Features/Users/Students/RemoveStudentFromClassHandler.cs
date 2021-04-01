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
    public class RemoveStudentFromClassCommand : IRequest
    {
        public Guid StudentId { get; set; }
    }
    
    public class RemoveStudentFromClassHandler : IRequestHandler<RemoveStudentFromClassCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IPublishEndpoint publishEndpoint;

        public RemoveStudentFromClassHandler(ClassRegisterContext context, IPublishEndpoint publishEndpoint)
        {
            this.context = context;
            this.publishEndpoint = publishEndpoint;
        }
        
        public async Task<Unit> Handle(RemoveStudentFromClassCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Students.Include(x => x.Class)
                    .ThenInclude(x => x.ClassSchoolYears)
                        .ThenInclude(x => x.ClassSchoolYearSubjects)
                .SingleAsync(x => x.Id == request.StudentId, cancellationToken);

            var @class = student.Class;
            student.Class = null;
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