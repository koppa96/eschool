using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Application.Features.Homeworks.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkCreateCommand : IRequest<HomeworkDetailsResponse>
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public DateTime Deadline { get; set; }
    }

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
        private readonly LessonService.LessonServiceClient client;
        private readonly HomeAssignmentsContext context;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public HomeworkCreateHandler(LessonService.LessonServiceClient client,
            HomeAssignmentsContext context,
            IMapper mapper,
            IIdentityService identityService)
        {
            this.client = client;
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        
        public async Task<HomeworkDetailsResponse> Handle(HomeworkCreateCommand request, CancellationToken cancellationToken)
        {
            var response = await client.ListStudentsAndTeachersForLessonAsync(new StudentAndTeacherListRequest
            {
                LessonId = request.LessonId.ToString()
            }, cancellationToken: cancellationToken);
            
            var studentGuids = response.StudentIds.Select(x => Guid.Parse(x)).ToList();
            var students = await context.Students.Where(x => studentGuids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var teacherGuids = response.TeacherIds.Select(x => Guid.Parse(x)).ToList();
            var teachers = await context.Teachers.Where(x => teacherGuids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            if (teachers.All(x => x.UserId != currentUserId))
            {
                throw new UnauthorizedAccessException(
                    "Csak olyan tanár írhat ki házi feladatot, aki tanítja az adott tárgyat az osztálynak a tanévben.");
            }

            var lesson = await context.Lessons.FindOrThrowAsync(request.LessonId, cancellationToken);

            var homework = new Homework
            {
                Title = request.Title,
                Description = request.Description,
                Optional = request.Optional,
                CreatedAt = DateTime.Now,
                Deadline = request.Deadline,
                Lesson = lesson,
                StudentHomeworks = students.Select(x => new StudentHomework
                {
                    Student = x
                }).ToList(),
                TeacherHomeworks = teachers.Select(x => new TeacherHomework
                {
                    Teacher = x
                }).ToList()
            };

            context.Homeworks.Add(homework);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkDetailsResponse>(homework);
        }
    }
}