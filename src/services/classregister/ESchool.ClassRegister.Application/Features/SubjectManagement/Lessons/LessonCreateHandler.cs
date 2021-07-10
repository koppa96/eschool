using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.Domain.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonCreateValidator : AbstractValidator<LessonCreateCommand>
    {
        public LessonCreateValidator()
        {
            RuleFor(x => x.Body.EndsAt)
                .Must((command, time) => time > command.Body.StartsAt)
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
                .SingleAsync(x => x.Id == request.ClassId, cancellationToken);

            var classSchoolYear = @class.ClassSchoolYears.Single(x => x.SchoolYearId == request.SchoolYearId);
            if (classSchoolYear.ClassSchoolYearSubjects
                .SelectMany(x => x.Lessons)
                .Any(x => !(x.StartsAt > request.Body.EndsAt || x.EndsAt < request.Body.StartsAt)))
            {
                throw new InvalidOperationException("Ebben az időpontban már nem vehető fel óra ennek az osztálynak.");
            }

            if (request.Body.StartsAt < classSchoolYear.SchoolYear.StartsAt ||
                request.Body.EndsAt > classSchoolYear.SchoolYear.EndsAt)
            {
                throw new InvalidOperationException("Az órának a megadott tanév tanítási idején belül kell lennie.");
            }

            var classroom = await context.Classrooms.FindOrThrowAsync(request.Body.ClassroomId, cancellationToken);
            var lesson = new Lesson
            {
                Title = request.Body.Title,
                Description = request.Body.Description,
                StartsAt = request.Body.StartsAt,
                EndsAt = request.Body.EndsAt,
                ClassSchoolYearSubject = classSchoolYear.ClassSchoolYearSubjects.Single(x => x.SubjectId == request.SubjectId),
                Classroom = classroom
            };

            context.Lessons.Add(lesson);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<LessonDetailsResponse>(lesson);
        }
    }
}