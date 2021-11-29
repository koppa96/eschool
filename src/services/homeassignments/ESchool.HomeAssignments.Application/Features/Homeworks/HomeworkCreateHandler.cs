using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.Libs.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkCreateValidator : AbstractValidator<HomeworkCreateCommand>
    {
        public HomeworkCreateValidator()
        {
            RuleFor(x => x.Deadline)
                .Must(x => x > DateTime.Now)
                .WithMessage("A határidőnek jövőbeli időpontnak kell lennie!");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Cím kitöltése kötelező!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Feladat leírás megadása kötelező!");
        }
    }
    
    public class HomeworkCreateHandler : IRequestHandler<HomeworkCreateCommand, HomeworkDetailsResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public HomeworkCreateHandler(
            HomeAssignmentsContext context,
            IMapper mapper,
            IIdentityService identityService)
        {
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        
        public async Task<HomeworkDetailsResponse> Handle(HomeworkCreateCommand request, CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectStudents)
                        .ThenInclude(x => x.Student)
                .Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                        .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.LessonId, cancellationToken);
            
            var currentUserId = identityService.GetCurrentUserId();
            if (lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x => x.Teacher.UserId != currentUserId))
            {
                throw new UnauthorizedAccessException(
                    "Csak olyan tanár írhat ki házi feladatot, aki tanítja az adott tárgyat az osztálynak a tanévben.");
            }
            
            var homework = new Homework
            {
                Title = request.Title,
                Description = request.Description,
                Optional = request.Optional,
                Deadline = request.Deadline,
                Lesson = lesson
            };

            context.Homeworks.Add(homework);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkDetailsResponse>(homework);
        }
    }
}