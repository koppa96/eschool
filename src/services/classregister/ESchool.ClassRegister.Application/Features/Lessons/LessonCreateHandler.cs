using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Lessons.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Lessons
{
    public class LessonCreateCommand : IRequest<LessonDetailsResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public Guid SchoolYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ClassroomId { get; set; }
    }

    public class LessonCreateValidator : AbstractValidator<LessonCreateCommand>
    {
        public LessonCreateValidator()
        {
            RuleFor(x => x.EndsAt)
                .Must((command, time) => time > command.StartsAt)
                .WithMessage("Az óra befejezésének időpontja a kezdete után kell, hogy essen.");
        }
    }
    
    public class LessonCreateHandler : IRequestHandler<LessonCreateCommand, LessonDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public LessonCreateHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<LessonDetailsResponse> Handle(LessonCreateCommand request, CancellationToken cancellationToken)
        {
            var @class = await context.Classes.Include(x => x.ClassSchoolYears)
                    .ThenInclude(x => x.ClassSchoolYearSubjects)
                        .ThenInclude(x => x.Lessons)
                .Include(x => x.ClassSchoolYears)
                    .ThenInclude(x => x.SchoolYear)
                .SingleAsync(x => x.Id == request.ClassId);

            var classSchoolYear = @class.ClassSchoolYears.Single(x => x.SchoolYearId == request.SchoolYearId);
            if (classSchoolYear.ClassSchoolYearSubjects
                .SelectMany(x => x.Lessons)
                .Any(x => !(x.StartsAt > request.EndsAt || x.EndsAt < request.StartsAt)))
            {
                throw new InvalidOperationException("Ebben az időpontban már nem vehető fel óra ennek az osztálynak.");
            }

            if (request.StartsAt < classSchoolYear.SchoolYear.StartsAt ||
                request.EndsAt > classSchoolYear.SchoolYear.EndsAt)
            {
                throw new InvalidOperationException("Az órának a megadott tanév tanítási idején belül kell lennie.");
            }

            var lesson = new Lesson
            {
                Title = request.Title,
                Description = request.Description,
                StartsAt = request.StartsAt,
                EndsAt = request.EndsAt,
                ClassSchoolYearSubject = classSchoolYear.ClassSchoolYearSubjects.Single(x => x.SubjectId == request.SubjectId),
                ClassroomId = request.ClassroomId
            };

            context.Lessons.Add(lesson);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<LessonDetailsResponse>(lesson);
        }
    }
}