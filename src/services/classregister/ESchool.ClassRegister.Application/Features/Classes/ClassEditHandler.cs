using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public ClassEditHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

            @class.HeadTeacher = newHeadTeacher;
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<ClassDetailsResponse>(@class);
        }
    }
}