using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceStateSetCommand
    {
        public AbsenceState AbsenceState { get; set; }
    }
    
    public class AbsenceStateSetHandler : IRequestHandler<EditCommand<AbsenceStateSetCommand>>
    {
        private readonly ClassRegisterContext context;

        public AbsenceStateSetHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(EditCommand<AbsenceStateSetCommand> request, CancellationToken cancellationToken)
        {
            var absence = await context.Absences.FindOrThrowAsync(request.Id, cancellationToken);
            absence.AbsenceState = request.InnerCommand.AbsenceState;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}