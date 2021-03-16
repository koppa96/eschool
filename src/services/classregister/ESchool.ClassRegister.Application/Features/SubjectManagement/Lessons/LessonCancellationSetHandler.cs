using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonCancellationSetCommand
    {
        public bool Canceled { get; set; }
    }
    
    public class LessonCancellationSetHandler : IRequestHandler<EditCommand<LessonCancellationSetCommand, LessonDetailsResponse>, LessonDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public LessonCancellationSetHandler(ClassRegisterContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<LessonDetailsResponse> Handle(EditCommand<LessonCancellationSetCommand, LessonDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.FindOrThrowAsync(request.Id, cancellationToken);
            lesson.Canceled = request.InnerCommand.Canceled;
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<LessonDetailsResponse>(lesson);
        }
    }
}