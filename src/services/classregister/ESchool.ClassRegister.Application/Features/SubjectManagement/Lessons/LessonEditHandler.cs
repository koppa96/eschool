using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonEditCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public bool Canceled { get; set; }
    }
    
    public class LessonEditHandler : IRequestHandler<EditCommand<LessonEditCommand, LessonDetailsResponse>, LessonDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public LessonEditHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<LessonDetailsResponse> Handle(EditCommand<LessonEditCommand, LessonDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(x => x.ClassSchoolYearSubject)
                .ThenInclude(x => x.ClassSchoolYear)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            var otherLessonsInSchoolYear = await context.Lessons.Where(x =>
                    x.ClassSchoolYearSubject.ClassSchoolYearId == lesson.ClassSchoolYearSubject.ClassSchoolYearId &&
                    x.Id != lesson.Id)
                .ToListAsync(cancellationToken);

            if (otherLessonsInSchoolYear.Any(x =>
                !(x.StartsAt > request.InnerCommand.EndsAt || x.EndsAt < request.InnerCommand.StartsAt)))
            {
                throw new InvalidOperationException("Ebben az időpontban már nem vehető fel óra ennek az osztálynak.");
            }

            lesson.Title = request.InnerCommand.Title;
            lesson.Canceled = request.InnerCommand.Canceled;
            lesson.Description = request.InnerCommand.Description;
            lesson.StartsAt = request.InnerCommand.StartsAt;
            lesson.EndsAt = request.InnerCommand.EndsAt;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<LessonDetailsResponse>(lesson);
        }
    }
}