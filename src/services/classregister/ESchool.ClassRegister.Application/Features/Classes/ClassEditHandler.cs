using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassEditCommand
    {
        public Guid HeadTeacherId { get; set; }
    }
    
    public class ClassEditHandler : IRequestHandler<EditCommand<ClassEditCommand, ClassDetailsResponse>, ClassDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public ClassEditHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<ClassDetailsResponse> Handle(EditCommand<ClassEditCommand, ClassDetailsResponse> request, CancellationToken cancellationToken)
        {
            var @class = await context.Classes.Include(x => x.ClassType)
                .Include(x => x.ClassSchoolYears)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            var newHeadTeacher = await context.Teachers.FindOrThrowAsync(request.InnerCommand.HeadTeacherId, cancellationToken);
            if (newHeadTeacher == null)
            {
                throw new InvalidOperationException("No teacher was found with the given id.");
            }
        }
    }
}