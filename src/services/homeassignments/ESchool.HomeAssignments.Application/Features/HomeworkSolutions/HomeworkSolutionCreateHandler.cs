using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions
{
    public class HomeworkSolutionCreateHandler : IRequestHandler<HomeworkSolutionCreateCommand, HomeworkSolutionResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public HomeworkSolutionCreateHandler(HomeAssignmentsContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkSolutionResponse> Handle(HomeworkSolutionCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var homework = await context.Homeworks.Include(x => x.Lesson)
                    .ThenInclude(x => x.ClassSchoolYearSubject)
                        .ThenInclude(x => x.ClassSchoolYearSubjectStudents)
                            .ThenInclude(x => x.Student)
                .Include(x => x.Solutions)
                    .ThenInclude(x => x.Student)
                .SingleAsync(x => x.Id == request.HomeworkId, cancellationToken);

            var student = homework.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectStudents
                .Where(x => x.Student.UserId == currentUserId)
                .Select(x => x.Student)
                .Single();
            
            if (homework.Solutions.Any(x => x.Student.UserId == currentUserId))
            {
                throw new InvalidOperationException(
                    "Már létrehozásra került egy megoldás az adott házi feladathoz ez által a diák által.");
            }
            
            if (homework.Deadline < DateTime.Now)
            {
                throw new InvalidOperationException("A határidő lejárt.");
            }

            var solution = new HomeworkSolution
            {
                Homework = homework,
                Student = student
            };

            context.HomeworkSolutions.Add(solution);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkSolutionResponse>(solution);
        }
    }
}